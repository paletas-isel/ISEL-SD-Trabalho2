using System;
using System.ServiceModel;
using System.Windows.Forms;
using ChatServiceProject;

namespace ChatWindowsApplication
{
    public partial class ChatForm : Form
    {
        public string Username { get; set; }

        public string Language { get; set; }

        public ChatServiceClient Tracker { get; private set; }

        public ChatForm()
        {
            InitializeComponent();
            Tracker = new ChatServiceClient(new InstanceContext(new MessageReceived(this)));
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            PickUsernameForm form = new PickUsernameForm(this);
            form.ShowDialog(this);
        }

        public void AddMessage(string userName, string content)
        {
            chat.Items.Add(String.Format("User {0} said {1}.", userName, content));
        }
    }

    public class MessageReceived : IChatServiceCallback
    {
        private readonly ChatForm _form;

        public MessageReceived(ChatForm form)
        {
            _form = form;
        }

        #region Implementation of IChatServiceCallback

        public void OnMensageReceived(ChatService.Message message)
        {
            _form.AddMessage(message.UserName, message.Content);
        }

        #endregion
    }
}
