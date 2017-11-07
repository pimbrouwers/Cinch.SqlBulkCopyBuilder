using Cinch.SqlBulkCopyBuilder;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

namespace Cinch.SqlBulkCopyBuilder.Tests
{
    public class SqlConnectionExtensionsTests
    {
        readonly string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [Fact]
        public void OpenConnection()
        {
            var conn = new SqlConnection(connStr);
            conn.Open();

            Assert.Equal(conn.State, ConnectionState.Open);
        }

        [Fact]
        public async Task OpenConnectionAsync()
        {
            var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            Assert.Equal(conn.State, ConnectionState.Open);
        }
    }
}
