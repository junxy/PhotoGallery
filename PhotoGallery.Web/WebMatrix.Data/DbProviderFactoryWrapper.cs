using System;
using System.Data.Common;
namespace WebMatrix.Data
{
	internal class DbProviderFactoryWrapper : IDbProviderFactory
	{
		private string _providerName;
		private DbProviderFactory _providerFactory;
		public DbProviderFactoryWrapper(string providerName)
		{
			this._providerName = providerName;
		}
		public DbConnection CreateConnection(string connectionString)
		{
			if (string.IsNullOrEmpty(this._providerName))
			{
				this._providerName = Database.GetDefaultProviderName();
			}
			if (this._providerFactory == null)
			{
				this._providerFactory = DbProviderFactories.GetFactory(this._providerName);
			}
			DbConnection dbConnection = this._providerFactory.CreateConnection();
			dbConnection.ConnectionString = connectionString;
			return dbConnection;
		}
	}
}
