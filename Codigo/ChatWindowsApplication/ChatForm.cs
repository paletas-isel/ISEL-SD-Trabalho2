using System;
using System.ServiceModel;
using System.Windows.Forms;
using ChatServiceProject;
using ChatWindowsApplication.ChatService;

namespace ChatWindowsApplication
{
    public partial class ChatForm : Form
    {
        public string Username { get; set; }

        public string Language { get; set; }

        public ChatServiceClient Tracker { get; private set; }

        private Uri _uri;

        public ChatForm(Uri uri)
        {
            InitializeComponent();
            _uri = uri;
            CreateTracker();
            Closing += (_, p) => Tracker.Unsubscribe();
        }

        public void CreateTracker()
        {
            Tracker = new ChatServiceClient(new InstanceContext(new MessageReceived(this)), new WSDualHttpBinding(), new EndpointAddress(_uri));
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            PickUsernameForm form = new PickUsernameForm(this);
            form.ShowDialog(this);
        }

        public void AddMessage(string userName, string content)
        {
            chat.Items.Add(String.Format("{0} said {1}.", userName, content));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Tracker.SendMessage(message.Text);
            }
            catch(CommunicationException)
            {
                CreateTracker();
                Tracker.SendMessage(message.Text);
            }
            AddMessage("You", message.Text);
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
