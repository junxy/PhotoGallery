using System;
namespace WebMatrix.Data
{
	internal class ConnectionConfiguration : IConnectionConfiguration
	{
		public IDbProviderFactory ProviderFactory
		{
			get;
			private set;
		}
		public string ConnectionString
		{
			get;
			private set;
		}
		internal ConnectionConfiguration(string providerName, string connectionString) : this(new DbProviderFactoryWrapper(providerName), connectionString)
		{
		}
		internal ConnectionConfiguration(IDbProviderFactory providerFactory, string connectionString)
		{
			this.ProviderFactory = providerFactory;
			this.ConnectionString = connectionString;
		}
	}
}
