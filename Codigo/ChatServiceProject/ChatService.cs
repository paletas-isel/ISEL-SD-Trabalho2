using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using ChatServiceProject.ChatServiceRemote;
using ChatServiceProject.Tracker;
using ChatServiceProject.Translator;

namespace ChatServiceProject
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        private IMessageReceivedChannel _clientCallback;
        private readonly LinkedList<ChatServiceClient> _chatMembers = new LinkedList<ChatServiceClient>();

        private User _user;
        private CentralServiceClient _client;

        private LanguageServiceClient _translator = new LanguageServiceClient();
        
        #region Implementation of IChatService

        public bool SendMessage(string message)
        {
            Console.WriteLine("Sending {0}.", message);

            Thread sendData = new Thread(() =>
            {
                foreach (var chatMember in _chatMembers)
                {
                    chatMember.ReceiveMessage(new ChatServiceRemote.Message { UserName = _user.Name, Content = message });
                }
            });

            sendData.Start();
            return true;
        }

        public bool ReceiveMessage(Message message)
        {
            Console.WriteLine("[SERVICE] Message \"{0}\" received from {1}.", message.Content, message.UserName);

            message.Content = _translator.Translate("F4E6E0444F32B660BED9908E9744594B53D2E864", message.Content, "", "en", "text/plain", "general");

            _clientCallback.OnMensageReceived(message);
            
            return true;    
        }

        public void Subscribe(string username)
        {
            _client = new CentralServiceClient(new InstanceContext(new RegisterUser(_chatMembers)));
            var callback = OperationContext.Current.GetCallbackChannel<IMessageReceivedChannel>();
            _clientCallback = callback;

            User self;

            IEnumerable<User> members = _client.LogOn("Sports", username, OperationContext.Current.EndpointDispatcher.EndpointAddress.Uri);

            self = members.ElementAt(0);
            members = members.Skip(1);

            InstanceContext context = new InstanceContext(new MessageReceived());
            WSDualHttpBinding binding = new WSDualHttpBinding();
            foreach (var s in members.Select((u, i) => new ChatServiceClient(   context,
                                                                                binding,
                                                                                new EndpointAddress(u.ChatService))))
            {
                _chatMembers.AddLast(s);
            }

            _user = self;
        }

        public void Unsubscribe()
        {
            _clientCallback = null;

            _client.Close();
        }

        public Theme[] GetThemes()
        {
            return _client.GetThemes();
        }

        #endregion
    }

    public class RegisterUser : ICentralServiceCallback
    {
        private readonly LinkedList<ChatServiceClient> _clients;

        public RegisterUser(LinkedList<ChatServiceClient> clients)
        {
            _clients = clients;
        }

        #region Implementation of IUserCallback

        public void OnUserJoined(User user)
        {
            Console.WriteLine("New user joined {0}.", user.Name);
            _clients.AddLast(new ChatServiceClient(new InstanceContext(new MessageReceived()), 
                                                   new WSDualHttpBinding(),
                                                   new EndpointAddress(user.ChatService)));
        }

        #endregion
    }

    public class MessageReceived : IChatServiceCallback
    {
        #region Implementation of IChatServiceCallback

        public void OnMensageReceived(ChatServiceRemote.Message message)
        {
            Console.WriteLine("[SERVICE1] Message \"{0}\" received from {1}.", message.Content, message.UserName); 
        }

        #endregion
    }
}