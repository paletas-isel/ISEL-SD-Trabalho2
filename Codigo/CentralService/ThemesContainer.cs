using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralService
{
    public class ThemesContainer
    {
        private readonly IDictionary<Theme, LinkedList<UserDecorator>> _themes;

        public ThemesContainer(IEnumerable<Theme> themes = null)
        {
            _themes = new Dictionary<Theme, LinkedList<UserDecorator>>();
            if(themes != null)
            {
                foreach(var theme in themes)
                {
                    _themes.Add(theme, new LinkedList<UserDecorator>());
                }
            }
        }

        public void AddTheme(Theme theme)
        {
            if(!_themes.ContainsKey(theme))
            {
                _themes.Add(theme, new LinkedList<UserDecorator>());
            }
            else
            {
                throw new InvalidOperationException("Theme already exists!");
            }
        }

        public void AddUser(Theme theme, UserDecorator user)
        {
            if (_themes.ContainsKey(theme))
            {
                var themeUserContainer = _themes[theme];
                if(!themeUserContainer.Contains(user))
                {
                    themeUserContainer.AddLast(user);
                }
                else
                {
                    throw new InvalidOperationException("User already exists!");
                }
            }
            else
            {
                throw new InvalidOperationException("Theme doesn't exist!");
            }
        }

        public IEnumerable<User> GetUsers(Theme theme)
        {
            if (_themes.ContainsKey(theme))
            {
                return _themes[theme];
            }
            throw new InvalidOperationException("Theme doesn't exist!");
        }

        public IEnumerable<Theme> GetThemes()
        {
            return _themes.Keys;
        }

        public Theme GetTheme(string name)
        {
            return _themes.Keys.SingleOrDefault(p => p.Name.Equals(name));
        }

        public UserDecorator GetUser(long id)
        {
            return _themes.Values.SelectMany(allThemeUsers => allThemeUsers).FirstOrDefault(user => user.Id == id);
        }

        public User GetUser(string name)
        {
            return _themes.Values.SelectMany(allThemeUsers => allThemeUsers).FirstOrDefault(user => user.Name.Equals(name));
        }

        public void RemoveUser(Theme theme, UserDecorator user)
        {
            _themes[theme].Remove(user);
        }
    }
}