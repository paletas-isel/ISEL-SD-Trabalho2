using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ChatService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChatService" in both code and config file together.
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        bool SendMessage(Mensage mensage);

        [OperationContract]
        void Subscribe();

        [OperationContract]
        void Unsubscribe();
    }

    public interface IMessageReceivedChannel
    {
        [OperationContract(IsOneWay = true)]
        void OnMensageReceived(Mensage mensage);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Mensage
    {
        public Mensage(string userName, string content)
        {
            UserName = userName;
            Content = content;
        }

        public string UserName { get; set; }

        public string Content { get; set; }
    }
}
