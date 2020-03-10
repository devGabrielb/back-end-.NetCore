using System;
using System.Data;
using System.Data.SqlClient;
using Mwa.Shared;

namespace Mwa.Infra.Contexts
{
    public class MwaDataContext : IDisposable
    {
        public SqlConnection Connection { get; set; }
        public IDbTransaction _transaction;
        public MwaDataContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
            _transaction = Connection.BeginTransaction();
        }

        
        public void Dispose()
        {
            if(Connection.State != ConnectionState.Closed)
                Connection.Close();
        }


       
    }
}
