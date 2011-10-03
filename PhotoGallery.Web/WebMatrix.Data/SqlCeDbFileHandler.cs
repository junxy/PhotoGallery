using System;
using System.Globalization;
using System.IO;
namespace WebMatrix.Data
{
	internal class SqlCeDbFileHandler : IDbFileHandler
	{
		private const string SqlCeConnectionStringFormat = "Data Source={0}";
		public IConnectionConfiguration GetConnectionConfiguration(string fileName)
		{
			string defaultProviderName = Database.GetDefaultProviderName();
			string connectionString = SqlCeDbFileHandler.GetConnectionString(fileName);
			return new ConnectionConfiguration(defaultProviderName, connectionString);
		}
		public static string GetConnectionString(string fileName)
		{
			if (Path.IsPathRooted(fileName))
			{
				return string.Format(CultureInfo.InvariantCulture, "Data Source={0}", new object[]
				{
					fileName
				});
			}
			string text = "|DataDirectory|\\" + Path.GetFileName(fileName);
			return string.Format(CultureInfo.InvariantCulture, "Data Source={0}", new object[]
			{
				text
			});
		}
	}
}
