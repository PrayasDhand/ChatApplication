using NuGet.Packaging.Signing;

namespace ChatApplication.Models
{
    public class Chats
    {
        int Guid {get;set;}
        public string SenderName { get;set;}
        public string RecieverName { get;set;}
        public int ChatsCount { get;set;}
        = 0;

        public Timestamp Timestamp { get;set;}

            

    }
}
