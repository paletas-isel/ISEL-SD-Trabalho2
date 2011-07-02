using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;

namespace ChatWindowsApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
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
                    MessageBox.Show("[SERVICE] Service is starting!");

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
                            MessageBox.Show("[SERVICE] Service is ending!");
                        }
                    }

                }
            });

            hosting.Start();
            mReset.Wait();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ChatForm form = new ChatForm(uri);
            form.Closed += (_, p) => hosting.Interrupt();

            Application.Run(form);
        }
    }
}
