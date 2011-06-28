using System.Runtime.Serialization;
using System.ServiceModel;

namespace ChatServiceProject
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChatService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IMessageReceivedChannel))]
    public interface IChatService
    {
        [OperationContract]
        bool SendMessage(string message);

        [OperationContract]
        bool ReceiveMessage(Message message);

        [OperationContract]
        void Subscribe(string username);

        [OperationContract]
        void Unsubscribe();
    }

    public interface IMessageReceivedChannel
    {
        [OperationContract(IsOneWay = true)]
        void OnMensageReceived(Message message);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Message
    {
        public Message(string userName, string content)
        {
            UserName = userName;
            Content = content;
        }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Content { get; set; }
    }
}