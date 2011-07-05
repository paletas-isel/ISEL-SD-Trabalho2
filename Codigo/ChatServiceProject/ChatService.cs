using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using ChatServiceProject.ChatServiceRemote;
using ChatServiceProject.Tracker;
using ChatServiceProject.Translator;
using Theme = ChatServiceProject.Tracker.Theme;

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

        private readonly LanguageServiceClient _translator = new LanguageServiceClient();
        private string _language;

        #region Implementation of IChatService

        public void SendMessage(string message)
        {
            Thread t = new Thread(a =>
                                      {
                                          lock (_chatMembers)
                                          {
                                              LinkedList<ChatServiceClient> toRemove = new LinkedList<ChatServiceClient>();
                                              foreach (var chatMember in _chatMembers)
                                              {
                                                  try
                                                  {
                                                      chatMember.ReceiveMessage(new ChatServiceRemote.Message { UserName = _user.Name, Content = message });
                                                  }
                                                  catch (Exception)
                                                  {
                                                      toRemove.AddLast(chatMember);
                                                  }
                                              }

                                              foreach (var chatMember in toRemove)
                                              {
                                                  _chatMembers.Remove(chatMember);
                                              }
                                          }
                                      });
            t.Start();
            
        }

        public void ReceiveMessage(Message message)
        {
            message.Content = _translator.Translate("F4E6E0444F32B660BED9908E9744594B53D2E864", message.Content, "", _language, "text/plain", "general");

            _clientCallback.OnMensageReceived(message);   
        }

        public void Subscribe(string username, string theme, string language)
        {
            _client = new CentralServiceClient(new InstanceContext(new RegisterUser(_chatMembers)));
            var callback = OperationContext.Current.GetCallbackChannel<IMessageReceivedChannel>();
            _clientCallback = callback;

            _language = language;

            User self;
            IEnumerable<User> members;
            try
            {
                members = _client.LogOn(theme, username, OperationContext.Current.EndpointDispatcher.EndpointAddress.Uri);
            }
            catch(FaultException<InvalidOperationException> e)
            {
                throw new FaultException<InvalidOperationException>(e.Detail);
            }

            self = members.ElementAt(0);
            members = members.Skip(1);

            InstanceContext context = new InstanceContext(new FakeCallback());
            WSDualHttpBinding binding = new WSDualHttpBinding();
            foreach (var s in members.Select((u, i) => new ChatServiceClient(   context,
                                                                                binding,
                                                                                new EndpointAddress(u.ChatService))))
            {
                lock(_chatMembers)
                {
                    _chatMembers.AddLast(s);
                }
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
            lock(_clients)
            {
                _clients.AddLast(new ChatServiceClient(new InstanceContext(new FakeCallback()),
                                                       new WSDualHttpBinding(),
                                                       new EndpointAddress(user.ChatService)));
            }
        }

        public void OnUserLeft(User user)
        {
            lock(_clients)
            {
                _clients.Remove(_clients.FirstOrDefault(c => c.Endpoint.ListenUri.Equals(user.ChatService)));
            }
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