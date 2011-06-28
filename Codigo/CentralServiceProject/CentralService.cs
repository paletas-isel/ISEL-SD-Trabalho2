using System;
using System.Linq;
using System.ServiceModel;

namespace CentralServiceProject
{
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

        public User[] LogOn(string themeName, string userName, out User self)
        {
            Console.WriteLine("LogOn Enter");

            if ((self = _container.GetUser(userName)) == null)
                self = new User(GenerateUniqueId(), userName, OperationContext.Current.Channel.RemoteAddress.Uri);

            var theme = _container.GetTheme(themeName);

            _container.AddUser(theme, new UserDecorator(self, OperationContext.Current.GetCallbackChannel<IUserCallback>()));

            Console.WriteLine("LogOn Exit");
            return _container.GetUsers(theme).ToArray();
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
