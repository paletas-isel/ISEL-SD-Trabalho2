using System;
using System.Linq;
using System.ServiceModel;

namespace CentralService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CentralService" in code, svc and config file together.
    public class CentralService : ICentralService
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

        public User[] LogOn(string themeName, string userName, Uri chatService, out User self)
        {
            if((self = _container.GetUser(userName)) == null)
                self = new User(GenerateUniqueId(), userName, chatService);

            var theme = _container.GetTheme(themeName);
            
            _container.AddUser(theme, new UserDecorator(self, OperationContext.Current.GetCallbackChannel<IUserCallback>()));

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
    }
}
