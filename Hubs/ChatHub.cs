using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Code;
using Code.DataAccess;

namespace light_chat_app.Hubs
{
    public class ChatHub : Hub
    {
        private readonly TranslateService _translateService;

        public ChatHub()
        {
            _translateService = new TranslateService();
        }

        public async Task SendMessage(string user, string originalMessage, string language)
        {
            //var message = _translateService.Translate(language, originalMessage);

            await new MessageRepository().Add(new MessageModel { Message = originalMessage });

            await Clients.All.SendAsync("ReceiveMessage", user, originalMessage);
        }
    }
}
