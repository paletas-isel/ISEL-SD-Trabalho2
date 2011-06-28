using System;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace CentralServiceProject
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    class CentralService : ICentralService
    {
        private long _currentId;
       
        private readonly ThemesContainer _container;

        public CentralService()
        {
            _container = new ThemesContainer(new[] { 
                new Theme("Sports", "Football, Tennis, ..."), 
                new Theme("Music", "Rock, Pop, ..."), 
                new Theme("Books", "Romance, Adventure, Fantasy, ...") 
            });
        }

        #region Implementation of ICentralService

        public Theme[] GetThemes()
        {
            return _container.GetThemes().ToArray();
        }

        public User[] LogOn(string themeName, string userName, Uri address)
        {
            Console.WriteLine("LogOn Enter");

            User self;
            if ((self = _container.GetUser(userName)) == null)
                self = new User(GenerateUniqueId(), userName, address);

            var theme = _container.GetTheme(themeName);

            self.Callback = OperationContext.Current.GetCallbackChannel<IUserCallback>();
            _container.AddUser(theme, self);

            var temp = new[] {self}.Concat(_container.GetUsers(theme).Where(u => !self.Equals(u))).ToArray();

            Console.WriteLine("Username {0} got {1} new users.", userName, temp.Length); 
            
            Thread t = new Thread(() =>
                                      {
                                         foreach(User user in _container.GetUsers(theme))
                                         {
                                             if(!user.Equals(self))
                                                user.Callback.OnUserJoined(self);
                                         }
                                      });

            t.Start();

            Console.WriteLine("LogOn Exit");

            return temp;
        }

        public void LogOff(string themeName, long id)
        {
            var user = _container.GetUser(id);
            var theme = _container.GetTheme(themeName);
            _container.RemoveUser(theme, user);
        }

        #endregion

        private long GenerateUniqueId()
        {
            return _currentId++;
        }

        public static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(CentralService)))
            {
                // Open the ServiceHost to create listeners and start listening for messages.
                serviceHost.Open();
                Console.WriteLine("[SERVICE] Service is starting!");

                Console.ReadLine();

                Console.WriteLine("[SERVICE] Service is ending!");
            }
        }
    }
}
