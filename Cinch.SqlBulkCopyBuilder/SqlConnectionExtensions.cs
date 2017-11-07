using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Cinch.SqlBulkCopyBuilder
{
    public static class SqlConnectionExtensions
    {
        public static void OpenConnection(this SqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        public static async Task OpenConnectionAsync(this SqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();
        }

        public static void Bulk<T>(this SqlConnection conn, SqlBulkCopyBuilder bcpBuilder, IEnumerable<T> srcData, IEnumerable<string> ignoreCols = null) where T : class, new()
        {
            conn.OpenConnection();

            using (var bcp = bcpBuilder.SetConnection(conn).Build())
            {
                bcp.MapColumns<T>(ignoreCols);

                using (var dataReader = ObjectReader.Create(srcData))
                {
                    bcp.WriteToServer(dataReader);
                }
            }
        }

        public static async Task BulkAsync<T>(this SqlConnection conn, SqlBulkCopyBuilder bcpBuilder, IEnumerable<T> srcData, IEnumerable<string> ignoreCols = null) where T : class, new()
        {
            await conn.OpenConnectionAsync();

            using (var bcp = bcpBuilder.SetConnection(conn).Build())
            {
                bcp.MapColumns<T>(ignoreCols);

                using (var dataReader = ObjectReader.Create(srcData))
                {
                    await bcp.WriteToServerAsync(dataReader);
                }
            }
        }
    }
}
