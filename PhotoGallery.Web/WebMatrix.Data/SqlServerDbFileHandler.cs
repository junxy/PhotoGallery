using System;
using System.Globalization;
using System.IO;
namespace WebMatrix.Data
{
	internal class SqlServerDbFileHandler : IDbFileHandler
	{
		private const string SqlServerConnectionStringFormat = "Data Source=.\\SQLEXPRESS;AttachDbFilename={0};Initial Catalog={1};Integrated Security=True;User Instance=True;MultipleActiveResultSets=True";
		private const string SqlServerProviderName = "System.Data.SqlClient";
		public IConnectionConfiguration GetConnectionConfiguration(string fileName)
		{
			return new ConnectionConfiguration("System.Data.SqlClient", SqlServerDbFileHandler.GetConnectionString(fileName, Database.DataDirectory));
		}
		internal static string GetConnectionString(string fileName, string dataDirectory)
		{
			if (Path.IsPathRooted(fileName))
			{
				return string.Format(CultureInfo.InvariantCulture, "Data Source=.\\SQLEXPRESS;AttachDbFilename={0};Initial Catalog={1};Integrated Security=True;User Instance=True;MultipleActiveResultSets=True", new object[]
				{
					fileName, 
					fileName
				});
			}
			string text = "|DataDirectory|\\" + Path.GetFileName(fileName);
			string text2 = Path.Combine(dataDirectory, Path.GetFileName(fileName));
			return string.Format(CultureInfo.InvariantCulture, "Data Source=.\\SQLEXPRESS;AttachDbFilename={0};Initial Catalog={1};Integrated Security=True;User Instance=True;MultipleActiveResultSets=True", new object[]
			{
				text, 
				text2
			});
		}
	}
}
