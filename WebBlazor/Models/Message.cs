using WebBlazor.Service;

namespace WebBlazor.Models
{
    public class Message
    {
        public string Info { get; set; }
        public string Description { get; set; }
        public MessagesType Type { get; set; }
    }
}
