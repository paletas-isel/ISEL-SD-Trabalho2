using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using ChatServiceProject.ChatServiceRemote;
using ChatServiceProject.Tracker;
using ChatServiceProject.Translator;
using Theme = ChatServiceProject.Tracker.Theme;
using UserFault = ChatServiceProject.ChatServiceRemote.UserFault;

namespace ChatServiceProject
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        private IMessageReceivedChannel _clientCallback;
        private readonly LinkedList<ChatServiceClient> _chatMembers = new LinkedList<ChatServiceClient>();

        private User _user;
        private string _theme;
        private CentralServiceClient _client;

        private LanguageServiceClient _translator = new LanguageServiceClient();
        private string _language;

        #region Implementation of IChatService

        public bool SendMessage(string message)
        {
            var sendData = new Thread(() =>
            {
                LinkedList<ChatServiceClient> toRemove = new LinkedList<ChatServiceClient>();
                foreach (var chatMember in _chatMembers)
                {
                    try
                    {
                        chatMember.ReceiveMessage(new ChatServiceRemote.Message { UserName = _user.Name, Content = message });
                    }
                    catch(CommunicationException)
                    {
                        toRemove.AddLast(chatMember);
                    }
                    catch(TimeoutException)
                    {
                        toRemove.AddLast(chatMember);
                    }
                    catch (ObjectDisposedException)
                    {
                        toRemove.Remove(chatMember);
                    }
                }

                foreach(var chatMember in toRemove)
                {
                    _chatMembers.Remove(chatMember);
                }
            });

            sendData.Start();
            return true;
        }

        public bool ReceiveMessage(Message message)
        {
            message.Content = _translator.Translate("F4E6E0444F32B660BED9908E9744594B53D2E864", message.Content, "", _language, "text/plain", "general");

            _clientCallback.OnMensageReceived(message);
            
            return true;    
        }

        public void Subscribe(string username, string theme, string language)
        {
            _client = new CentralServiceClient(new InstanceContext(new RegisterUser(_chatMembers)));
            var callback = OperationContext.Current.GetCallbackChannel<IMessageReceivedChannel>();
            _clientCallback = callback;

            _language = language;

            User self;

            IEnumerable<User> members = _client.LogOn(theme, username, OperationContext.Current.EndpointDispatcher.EndpointAddress.Uri);

            self = members.ElementAt(0);
            members = members.Skip(1);

            InstanceContext context = new InstanceContext(new FakeCallback());
            WSDualHttpBinding binding = new WSDualHttpBinding();
            foreach (var s in members.Select((u, i) => new ChatServiceClient(   context,
                                                                                binding,
                                                                                new EndpointAddress(u.ChatService))))
            {
                _chatMembers.AddLast(s);
            }

            _theme = theme;
            _user = self;
        }

        public void Unsubscribe()
        {
            _clientCallback = null;
            
            try
            {
                _client.LogOff(_theme, _user.Id);
            }
            catch(CommunicationException)
            {
                _client = new CentralServiceClient(new InstanceContext(new RegisterUser(_chatMembers)));
                _client.LogOff(_theme, _user.Id);
            }

            _client.Close();
        }

        public Theme[] GetThemes()
        {
            if (_client == null) 
                _client = new CentralServiceClient(new InstanceContext(new RegisterUser(_chatMembers)));
            
            try
            {
                return _client.GetThemes();
            }
            catch(CommunicationException)
            {
                _client = new CentralServiceClient(new InstanceContext(new RegisterUser(_chatMembers)));
                return _client.GetThemes();
            }
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
            _clients.AddLast(new ChatServiceClient(new InstanceContext(new FakeCallback()), 
                                                   new WSDualHttpBinding(),
                                                   new EndpointAddress(user.ChatService)));
        }

        #endregion
    }

    public class FakeCallback : IChatServiceCallback
    {
        #region Implementation of IChatServiceCallback

        public void OnMensageReceived(ChatServiceRemote.Message message)
        {
        }

        #endregion
    }
}