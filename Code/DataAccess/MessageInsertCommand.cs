using System.Configuration;
using Dapper;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Code.DataAccess
{
    public class MessageInsertCommand
    {
        private readonly MessageModel _messageModel;

        private readonly string _connectionString;

        public MessageInsertCommand(MessageModel messageModel)
        {
            _connectionString = "Host=127.0.0.1;Database=chat_db;Uid=user;Pwd=user;";
            _messageModel = messageModel;
        }

        public async Task ExecuteAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"
INSERT INTO chat_history (message)
VALUES (@message)";

                await connection.ExecuteAsync(sql, _messageModel);
            }
            
        }
    }
}
