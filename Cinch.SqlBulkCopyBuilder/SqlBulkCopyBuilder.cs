using System;
using System.Data.SqlClient;

namespace Cinch.SqlBulkCopyBuilder
{
    public class SqlBulkCopyBuilder
    {
        SqlConnection conn;
        string destinationTableName;
        int batchSize = 0;
        int bulkCopyTimeout = 30;
        SqlBulkCopyOptions sqlBulkCopyOptions;
        SqlTransaction transaction;

        // CONVERSION OPERATOR
        public SqlBulkCopy Build()
        {
            if (this.conn == null)
            {
                throw new ArgumentException("sqlConnection cannot be null. SetConnection(SqlConnection conn) must be called before Build().");
            }
            else if (string.IsNullOrWhiteSpace(destinationTableName))
            {
                throw new ArgumentException("destinationTableName cannot be null. SetDestinationTable(string destinationTableName) must be called before Build().");
            }

            SqlBulkCopy bcp = new SqlBulkCopy(this.conn, this.sqlBulkCopyOptions, this.transaction);
            
            bcp.DestinationTableName = this.destinationTableName;
            bcp.BatchSize = this.batchSize;
            bcp.BulkCopyTimeout = this.bulkCopyTimeout;

            return bcp;
        }

        public SqlBulkCopyBuilder SetDestinationTable(string destinationTableName)
        {
            this.destinationTableName = destinationTableName;

            return this;
        }

        public SqlBulkCopyBuilder SetConnection(SqlConnection conn)
        {
            if (this.conn == null)
                this.conn = conn;

            return this;
        }

        public SqlBulkCopyBuilder SetBatchSize(int batchSize)
        {
            this.batchSize = batchSize;
            return this;
        }

        public SqlBulkCopyBuilder SetBulkCopyTimeout(int bulkCopyTimeout)
        {
            this.bulkCopyTimeout = bulkCopyTimeout;
            return this;
        }

        public SqlBulkCopyBuilder WithOptions(SqlBulkCopyOptions sqlBulkCopyOptions)
        {
            this.sqlBulkCopyOptions = sqlBulkCopyOptions;
            return this;
        }

        public SqlBulkCopyBuilder UsingTransaction(SqlTransaction transaction)
        {
            this.transaction = transaction;
            return this;
        }
    }
}
