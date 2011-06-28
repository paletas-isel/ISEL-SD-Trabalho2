using System;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using ChatServiceProject.ChatServiceRemote;

namespace ChatConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ManualResetEventSlim mReset = new ManualResetEventSlim();
            Uri uri = null;

            Thread hosting = new Thread(() =>
                                            {
                                                #if DEBUG

                                                    Thread.CurrentThread.Name = "ServiceHost";

                                                #endif    

                                                using (ServiceHost serviceHost = new ServiceHost(typeof(ChatServiceProject.ChatService)))
                                                {
                                                    // Open the ServiceHost to create listeners and start listening for messages.
                                                    serviceHost.Open();
                                                    Console.WriteLine("[SERVICE] Service is starting!");

                                                    uri =
                                                        serviceHost.Description.Endpoints.SingleOrDefault(
                                                            p => p.Contract.Name != "IMetadataExchange").Address.Uri;

                                                    mReset.Set();

                                                    lock (serviceHost)
                                                    {
                                                        try
                                                        {
                                                            Monitor.Wait(serviceHost);
                                                        }
                                                        catch (ThreadInterruptedException)
                                                        {
                                                            Console.WriteLine("[SERVICE] Service is ending!");
                                                        }
                                                    }

                                                }
                                            });

            hosting.Start();

            mReset.Wait();

            ChatServiceClient chatClient = new ChatServiceClient(new InstanceContext(new MessageCallback()), new WSDualHttpBinding(), new EndpointAddress(uri));

            Console.Write("User: ");
            string user = Console.ReadLine();
            chatClient.Subscribe(user);

            string read;
            do
            {
                Console.Write("Message: ");
                read = Console.ReadLine();

                if (read.Equals("exit"))
                    break;

                chatClient.SendMessage(read);
            } while (true);

            chatClient.Unsubscribe();
            hosting.Interrupt();
        }
    }

    public class MessageCallback : IChatServiceCallback
    {
        #region Implementation of IMessageReceivedChannel

        public void OnMensageReceived(Message message)
        {
            Console.WriteLine(string.Format("User: {0} - Message: {1}", message.UserName, message.Content));
        }

        #endregion
    }
}
