using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ChatServiceProject.ChatServiceRemote;
using ChatServiceProject.Tracker;

namespace ChatServiceProject
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class ChatService : IChatService
    {
        private IMessageReceivedChannel _consumers;
        private readonly LinkedList<ChatServiceClient> _chatMembers = new LinkedList<ChatServiceClient>();

        private User _user;
        private CentralServiceClient _client;

        #region Implementation of IChatService

        public bool SendMessage(string message)
        {
            Console.WriteLine("Sending {0}.", message);

            foreach (var chatMember in _chatMembers)
            {
                chatMember.ReceiveMessage(new ChatServiceRemote.Message { UserName = _user.Name, Content = message });
            }

            return true;
        }

        public bool ReceiveMessage(Message message)
        {
            _consumers.OnMensageReceived(message);

            return true;
        }

        public void Subscribe(string username)
        {
            _client = new CentralServiceClient(new InstanceContext(new RegisterUser()));
            var callback = OperationContext.Current.GetCallbackChannel<IMessageReceivedChannel>();
            _consumers = callback;

            User self;

            IEnumerable<User> members = _client.LogOn("Sports", username);

            self = members.ElementAt(0);
            members = members.Skip(1);

            foreach (var member in members.Select(p => new ChatServiceClient(new InstanceContext(callback),
                                                                      new WSDualHttpBinding(),
                                                                      new EndpointAddress(p.ChatService))))
            {
                _chatMembers.AddLast(member);
            }

            _user = self;
        }

        public void Unsubscribe()
        {
            _consumers = null;

            _client.Close();
        }

        #endregion
    }

    public class RegisterUser : ICentralServiceCallback
    {
        #region Implementation of IUserCallback

        public void OnUserJoined(User user)
        {
            Console.WriteLine("New user joined {0}.", user.Name);
        }

        #endregion
    }
}