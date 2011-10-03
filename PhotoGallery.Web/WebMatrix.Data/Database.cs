
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Internal.Web.Utils;
using WebMatrix.Data.Resources;

namespace WebMatrix.Data
{
	public class Database : IDisposable
	{
		internal const string SqlCeProviderName = "System.Data.SqlServerCe.4.0";
		internal const string SqlServerProviderName = "System.Data.SqlClient";
		private const string DefaultDataProviderAppSetting = "systemData:defaultProvider";
		internal static string DataDirectory = ((string)AppDomain.CurrentDomain.GetData("DataDirectory")) ?? Directory.GetCurrentDirectory();
		private Func<DbConnection> _connectionFactory;
		private DbConnection _connection;
		private static readonly IDictionary<string, IDbFileHandler> _dbFileHandlers = new Dictionary<string, IDbFileHandler>(StringComparer.OrdinalIgnoreCase)
		{

			{
				".sdf", 
				new SqlCeDbFileHandler()
			}, 

			{
				".mdf", 
				new SqlServerDbFileHandler()
			}
		};
		private static readonly IConfigurationManager _configurationManager = new ConfigurationManagerWrapper(Database._dbFileHandlers, null);
		private static event EventHandler<ConnectionEventArgs> _connectionOpened;
		public static event EventHandler<ConnectionEventArgs> ConnectionOpened
		{
			add
			{
				Database._connectionOpened += value;
			}
			remove
			{
				Database._connectionOpened -= value;
			}
		}
		public DbConnection Connection
		{
			get
			{
				if (this._connection == null)
				{
					this._connection = this._connectionFactory();
				}
				return this._connection;
			}
		}
		internal Database(Func<DbConnection> connectionFactory)
		{
			this._connectionFactory = connectionFactory;
		}
		public void Close()
		{
			this.Dispose();
		}
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._connection != null)
			{
				this._connection.Close();
				this._connection = null;
			}
		}
		//[return: Dynamic]
		public dynamic QuerySingle(string commandText, params object[] args)
		{
			if (string.IsNullOrEmpty(commandText))
			{
				throw ExceptionHelper.CreateArgumentNullOrEmptyException("commandText");
			}
			return this.QueryInternal(commandText, args).FirstOrDefault<object>();
		}
        //[return: Dynamic(new bool[]
        //{
        //    false, 
        //    true
        //})]
		public IEnumerable<dynamic> Query(string commandText, params object[] parameters)
		{
			if (string.IsNullOrEmpty(commandText))
			{
				throw ExceptionHelper.CreateArgumentNullOrEmptyException("commandText");
			}
			return this.QueryInternal(commandText, parameters).ToList<object>().AsReadOnly();
		}
        //[return: Dynamic(new bool[]
        //{
        //    false, 
        //    true
        //})]
		private IEnumerable<dynamic> QueryInternal(string commandText, params object[] parameters)
		{
			this.EnsureConnectionOpen();
			DbCommand dbCommand = this.Connection.CreateCommand();
			dbCommand.CommandText = commandText;
			Database.AddParameters(dbCommand, parameters);
			using (dbCommand)
			{
				IEnumerable<string> enumerable = null;
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					foreach (DbDataRecord record in dbDataReader)
					{
						if (enumerable == null)
						{
							enumerable = Database.GetColumnNames(record);
						}
						yield return new DynamicRecord(enumerable, record);
					}
				}
			}
			yield break;
		}
		private static IEnumerable<string> GetColumnNames(DbDataRecord record)
		{
			for (int i = 0; i < record.FieldCount; i++)
			{
				yield return record.GetName(i);
			}
			yield break;
		}
		public int Execute(string commandText, params object[] args)
		{
			if (string.IsNullOrEmpty(commandText))
			{
				throw ExceptionHelper.CreateArgumentNullOrEmptyException("commandText");
			}
			this.EnsureConnectionOpen();
			DbCommand dbCommand = this.Connection.CreateCommand();
			dbCommand.CommandText = commandText;
			Database.AddParameters(dbCommand, args);
			int result;
			using (dbCommand)
			{
				result = dbCommand.ExecuteNonQuery();
			}
			return result;
		}
		//[return: Dynamic]
		public dynamic GetLastInsertId()
		{
			return this.QueryValue("SELECT @@Identity", new object[0]);
		}
		//[return: Dynamic]
		public dynamic QueryValue(string commandText, params object[] args)
		{
			if (string.IsNullOrEmpty(commandText))
			{
				throw ExceptionHelper.CreateArgumentNullOrEmptyException("commandText");
			}
			this.EnsureConnectionOpen();
			DbCommand dbCommand = this.Connection.CreateCommand();
			dbCommand.CommandText = commandText;
			Database.AddParameters(dbCommand, args);
			object result;
			using (dbCommand)
			{
				result = dbCommand.ExecuteScalar();
			}
			return result;
		}
		private void EnsureConnectionOpen()
		{
			if (this.Connection.State != ConnectionState.Open)
			{
				this.Connection.Open();
				this.OnConnectionOpened();
			}
		}
		private void OnConnectionOpened()
		{
			if (Database._connectionOpened != null)
			{
				Database._connectionOpened(this, new ConnectionEventArgs(this.Connection));
			}
		}
		private static void AddParameters(DbCommand command, object[] args)
		{
			if (args == null)
			{
				return;
			}
			IEnumerable<DbParameter> enumerable = args.Select(delegate(object o, int index)
			{
				DbParameter dbParameter = command.CreateParameter();
				dbParameter.ParameterName = index.ToString(CultureInfo.InvariantCulture);
				dbParameter.Value = o;
				return dbParameter;
			}
			);
			foreach (DbParameter current in enumerable)
			{
				command.Parameters.Add(current);
			}
		}
		public static Database OpenConnectionString(string connectionString)
		{
			string providerName = null;
			return Database.OpenConnectionString(connectionString, providerName);
		}
		public static Database OpenConnectionString(string connectionString, string providerName)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				throw ExceptionHelper.CreateArgumentNullOrEmptyException("connectionString");
			}
			return Database.OpenConnectionStringInternal(providerName, connectionString);
		}
		public static Database Open(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw ExceptionHelper.CreateArgumentNullOrEmptyException("name");
			}
			return Database.OpenNamedConnection(name, Database._configurationManager);
		}
		internal static IConnectionConfiguration GetConnectionConfiguration(string fileName, IDictionary<string, IDbFileHandler> handlers)
		{
			string extension = Path.GetExtension(fileName);
			IDbFileHandler dbFileHandler;
			if (handlers.TryGetValue(extension, out dbFileHandler))
			{
				return dbFileHandler.GetConnectionConfiguration(fileName);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, DataResources.UnableToDetermineDatabase, new object[]
			{
				fileName
			}));
		}
		private static Database OpenConnectionStringInternal(string providerName, string connectionString)
		{
			return Database.OpenConnectionStringInternal(new DbProviderFactoryWrapper(providerName), connectionString);
		}
		private static Database OpenConnectionInternal(IConnectionConfiguration connectionConfig)
		{
			return Database.OpenConnectionStringInternal(connectionConfig.ProviderFactory, connectionConfig.ConnectionString);
		}
		internal static Database OpenConnectionStringInternal(IDbProviderFactory providerFactory, string connectionString)
		{
			return new Database(() => providerFactory.CreateConnection(connectionString));
		}
		internal static Database OpenNamedConnection(string name, IConfigurationManager configurationManager)
		{
			IConnectionConfiguration connection = configurationManager.GetConnection(name);
			if (connection != null)
			{
				return Database.OpenConnectionInternal(connection);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, DataResources.ConnectionStringNotFound, new object[]
			{
				name
			}));
		}
		internal static string GetDefaultProviderName()
		{
			string result;
			if (!Database._configurationManager.AppSettings.TryGetValue("systemData:defaultProvider", out result))
			{
				result = "System.Data.SqlServerCe.4.0";
			}
			return result;
		}
	}
}
