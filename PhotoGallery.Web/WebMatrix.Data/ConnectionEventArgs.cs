using System;
using System.Data.Common;
namespace WebMatrix.Data
{
	public class ConnectionEventArgs : EventArgs
	{
		public DbConnection Connection
		{
			get;
			private set;
		}
		public ConnectionEventArgs(DbConnection connection)
		{
			this.Connection = connection;
		}
	}
}
