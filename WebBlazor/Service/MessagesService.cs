using WebBlazor.Models;

namespace WebBlazor.Service
{
    public class MessagesService
    {
        public event Action OnMessageRefresh;
        private Message _message;
        public Message Message
        {
            get => _message;
            set => _message = value;
        }

        public void ShowMessage(Message message)
        {
            _message = message;
            OnMessageRefresh?.Invoke();
        }

        public async Task ShowMessage(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                ShowMessage(new Message()
                {
                    Info = response.StatusCode.ToString(),
                    Description = await response.Content.ReadAsStringAsync(),
                    Type = MessagesType.Success
                });
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                ShowMessage(new Message()
                {
                    Info = error.Message,
                    Description = error.Error,
                    Type = MessagesType.Error
                });
            }
        }
    }
    public enum MessagesType
    {
        Success,
        Error
    }
}


