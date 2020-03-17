using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code.DataAccess
{
    public class MessageRepository
    {
        public MessageRepository()
        {

        }

        public async Task<List<MessageModel>> GetAll()
        {
            return await new MessageQuery().GetAllAsync();
        }

        public async Task Add(MessageModel message)
        {
            await new MessageInsertCommand(message).ExecuteAsync();
        }
    }
}
