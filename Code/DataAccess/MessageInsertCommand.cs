using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Code.DataAccess
{
    public class MessageInsertCommand
    {
        private readonly MessageModel _message;

        private readonly string _connectionString;

        public MessageInsertCommand(MessageModel messageModel)
        {
            _connectionString = "Host=127.0.0.1;Database=chat_db;Uid=user;Pwd=user;";
            _message = messageModel;
        }

        public async Task ExecuteAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"
INSERT INTO chat_history (message)
VALUES (@message)";

                await connection.ExecuteAsync(sql,
                    new
                    {
                        message = _message.Message
                    });
            }
            
        }
    }
}
