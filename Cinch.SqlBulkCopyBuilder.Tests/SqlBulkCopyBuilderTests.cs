using System;
using System.Data.SqlClient;
using Xunit;

namespace Cinch.SqlBulkCopyBuilder.Tests
{
    public class SqlBulkCopyBuilderTests
    {
        readonly string connStr = "Data Source=(LocalDB)\v11.0;";

        [Fact]
        public void EmptyBuilder()
        {
            var sqlBulkCopyBuilder = new SqlBulkCopyBuilder();

            Assert.Throws<ArgumentException>(() => sqlBulkCopyBuilder.Build());
        }

        [Fact]
        public void SetConnection()
        {
            var conn = new SqlConnection(connStr);
            var sqlBulkCopyBuilder = new SqlBulkCopyBuilder().SetConnection(conn);

            Assert.Throws<ArgumentException>(() => sqlBulkCopyBuilder.Build());
        }

        [Fact]
        public void SetConnectionAndDestinationTableName()
        {
            var conn = new SqlConnection(connStr);
            string tableName = "dbo.Test";
            var sqlBulkCopyBuilder = new SqlBulkCopyBuilder().SetConnection(conn).SetDestinationTable(tableName);

            var bcp = sqlBulkCopyBuilder.Build();

            Assert.Equal(tableName, bcp.DestinationTableName);
        }

        [Fact]
        public void SetBatchSize()
        {
            var conn = new SqlConnection(connStr);
            string tableName = "dbo.Test";
            int batchSize = 1000;
            var sqlBulkCopyBuilder = new SqlBulkCopyBuilder().SetConnection(conn).SetDestinationTable(tableName).SetBatchSize(batchSize);

            var bcp = sqlBulkCopyBuilder.Build();

            Assert.Equal(batchSize, bcp.BatchSize);
        }

        [Fact]
        public void SetBulkCopyTimeout()
        {
            var conn = new SqlConnection(connStr);
            string tableName = "dbo.Test";
            int bulkCopyTimeout = 60;
            var sqlBulkCopyBuilder = new SqlBulkCopyBuilder().SetConnection(conn).SetDestinationTable(tableName).SetBulkCopyTimeout(bulkCopyTimeout);

            var bcp = sqlBulkCopyBuilder.Build();

            Assert.Equal(bulkCopyTimeout, bcp.BulkCopyTimeout);
        }
    }
}
