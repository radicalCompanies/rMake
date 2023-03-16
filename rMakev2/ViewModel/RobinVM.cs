using rMakev2.Services;
using System.ComponentModel;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;

namespace rMakev2.ViewModel
{
    public class RobinVM : INotifyPropertyChanged
    {
        private AIChat _aiChat;
        readonly IJSRuntime _js;
        public RobinVM(AIChat aiChat, IJSRuntime jsruntime)
        {
            this._aiChat = aiChat;
            _js = jsruntime;
            History = new List<ChatHistory>();
        }

        private List<ChatHistory> _history;
        public List<ChatHistory> History
        {
            get { return _history; }
            set
            {
                _history = value;
                OnPropertyChanged();
            }

        }

        public bool Typing { get; set; }
        
        private string textInput;
        public string TextInput
        {
            get { return textInput; }
            set
            {
                textInput = value;
                OnPropertyChanged();
            }
        }

        public async Task CallJsFn()
        {
            await _js.InvokeAsync<object>("scrollToEnd");
        }

        public string response;

        public async Task SendMessage()
        {

                       
            Typing = true;
            History.Add(new ChatHistory { Text = TextInput, Type = 0 });
            response = await _aiChat.UseChatService("Using maximum 100 words answer this: " + TextInput);
            TextInput = "";
            History.Add(new ChatHistory { Text = response, Type = 1 });
            



        }

        public async Task KeyPress(KeyboardEventArgs e)
        {

            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                
                await SendMessage();


            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }

    public class ChatHistory
    {
        public string Text { get; set; }
        public int Type { get; set; }
    }


}
