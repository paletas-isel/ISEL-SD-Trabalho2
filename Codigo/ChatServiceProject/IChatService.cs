using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using ChatServiceProject.Tracker;

namespace ChatServiceProject
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChatService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IMessageReceivedChannel))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);

        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(Message message);

        [OperationContract]
        [FaultContract(typeof(InvalidOperationException))]
        void Subscribe(string username, string theme, string language);

        [OperationContract]
        [FaultContract(typeof(InvalidOperationException))]
        void Unsubscribe();

        [OperationContract]
        Theme[] GetThemes();
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