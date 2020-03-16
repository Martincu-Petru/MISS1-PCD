using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Code.DataAccess
{
    public class MessageQuery
    {
        private readonly string _connectionString;

        public MessageQuery()
        {
            _connectionString = "Host=127.0.0.1;Database=chat_db;Uid=user;Pwd=user;";
        }

        public async Task<List<MessageModel>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"
SELECT 
    Message
FROM
    chat_history
ORDER BY Id DESC";

                var result = await connection.QueryAsync<MessageModel>(sql);

                return result.ToList();
            }
        }
    }
}
