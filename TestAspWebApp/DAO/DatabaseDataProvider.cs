using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using TestAspWebApp.Model;

namespace TestAspWebApp.DAO
{
    /// <summary>
    /// Провайдер доступа к базе данных. Используется только MS SQL Server.
    /// </summary>
    public class DatabaseDataProvider : BaseDataProvider
    {
        private readonly string dbConnectionString;
        
        public DatabaseDataProvider()
        {
            dbConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(dbConnectionString);
        }

        public override void Save(IEnumerable<OrderItem> items)
        {
            const string UNION = " union all ";
            ExecuteSql("delete from OrderItems");
            if (items != null && items.Any())
            {
                var sql = items.Aggregate("insert into OrderItems(Code, Description, Quantity, Price)",
                    (current, item) => current + ("select " + item.Code + ",'" + item.Description + "'," + item.Quantity + "," + item.Price + UNION));
                sql = sql.Substring(0, sql.Length - UNION.Length);
                ExecuteSql(sql);
            }
        }

        public override IEnumerable<OrderItem> Read()
        {
            var dataTable = ReceiveSqlResults("select Code, Description, Quantity, Price from OrderItems");
            return (from DataRow row in dataTable.Rows
                select new OrderItem
                {
                    Code = int.Parse(row["Code"].ToString()), 
                    Description = row["Description"].ToString(), 
                    Price = float.Parse(row["Price"].ToString(), CultureInfo.InvariantCulture), 
                    Quantity = float.Parse(row["Quantity"].ToString(), CultureInfo.InvariantCulture)
                });
        }

        private DataTable ReceiveSqlResults(string sqlCommand)
        {
            var dt = new DataTable();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(sqlCommand, conn);
                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException("Error during connection to the DataBase.", e);
            }
            return dt;
        }

        private void ExecuteSql(string sqlCommand)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(sqlCommand, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException("Error during connection to the DataBase.", e);
            }
        }
    }
}