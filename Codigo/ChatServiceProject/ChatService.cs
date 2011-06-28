using System.Collections.Generic;
using System.ServiceModel;
using ChatServiceProject.ChatServiceRemote;
using ChatServiceProject.Tracker;

namespace ChatServiceProject
{
    public class ChatService : IChatService
    {
        private IMessageReceivedChannel _consumers;
        private readonly LinkedList<ChatServiceClient> _chatMembers = new LinkedList<ChatServiceClient>();

        private User _user;

        #region Implementation of IChatService

        public bool SendMessage(string message)
        {
            foreach (var chatMember in _chatMembers)
            {
                chatMember.ReceiveMessage(new ChatServiceProject.ChatServiceRemote.Message { UserName = _user.Name, Content = message });
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
            var callback = OperationContext.Current.GetCallbackChannel<IMessageReceivedChannel>();
            _consumers = callback;

            var client = new CentralServiceClient(new InstanceContext(new RegisterUser()));
            User self;

            User[] members = client.LogOn(out self, "Sports", username);

            //foreach(var member in members.Select(p => new ChatServiceClient(new InstanceContext(callback), 
            //                                                          new WSDualHttpBinding(),
            //                                                          new EndpointAddress(p.ChatService))))
            //{
            //    _chatMembers.AddLast(member);
            //}

            _user = self;

            client.Close();
        }

        public void Unsubscribe()
        {
            _consumers = null;
        }

        #endregion
    }

    public class RegisterUser : ICentralServiceCallback
    {
        #region Implementation of IUserCallback

        public void OnUserJoined(User user)
        {

        }

        #endregion
    }
}