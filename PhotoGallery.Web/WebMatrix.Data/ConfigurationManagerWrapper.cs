using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
namespace WebMatrix.Data
{
	internal class ConfigurationManagerWrapper : IConfigurationManager
	{
		private IDictionary<string, string> _appSettings;
		private IDictionary<string, IDbFileHandler> _handlers;
		private readonly string _dataDirectory;
		public IDictionary<string, string> AppSettings
		{
			get
			{
				if (this._appSettings == null)
				{
					this._appSettings = (
						from string key in ConfigurationManager.AppSettings
						select key).ToDictionary((string key) => key, (string key) => ConfigurationManager.AppSettings[key]);
				}
				return this._appSettings;
			}
		}
		public ConfigurationManagerWrapper(IDictionary<string, IDbFileHandler> handlers, string dataDirectory = null)
		{
			this._dataDirectory = (dataDirectory ?? Database.DataDirectory);
			this._handlers = handlers;
		}
		private static IConnectionConfiguration GetConnectionConfigurationFromConfig(string name)
		{
			ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[name];
			if (connectionStringSettings != null)
			{
				return new ConnectionConfiguration(connectionStringSettings.ProviderName, connectionStringSettings.ConnectionString);
			}
			return null;
		}
		public IConnectionConfiguration GetConnection(string name)
		{
			return this.GetConnection(name, new Func<string, IConnectionConfiguration>(ConfigurationManagerWrapper.GetConnectionConfigurationFromConfig), new Func<string, bool>(File.Exists));
		}
		internal IConnectionConfiguration GetConnection(string name, Func<string, IConnectionConfiguration> getConfigConnection, Func<string, bool> fileExists)
		{
			IConnectionConfiguration connectionConfiguration = getConfigConnection(name);
			if (connectionConfiguration != null)
			{
				return connectionConfiguration;
			}
			foreach (KeyValuePair<string, IDbFileHandler> current in 
				from h in this._handlers
				orderby h.Key
				select h)
			{
				string text = Path.Combine(this._dataDirectory, name + current.Key);
				if (fileExists(text))
				{
					return current.Value.GetConnectionConfiguration(text);
				}
			}
			return null;
		}
	}
}
