using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralServiceProject
{
    public class ThemesContainer
    {
        private readonly IDictionary<Theme, LinkedList<User>> _themes;

        public ThemesContainer(IEnumerable<Theme> themes = null)
        {
            _themes = new Dictionary<Theme, LinkedList<User>>();
            if(themes != null)
            {
                foreach(var theme in themes)
                {
                    _themes.Add(theme, new LinkedList<User>());
                }
            }
        }

        public void AddTheme(Theme theme)
        {
            lock (this)
            {
                if (!_themes.ContainsKey(theme))
                {
                    _themes.Add(theme, new LinkedList<User>());
                }
                else
                {
                    throw new InvalidOperationException("Theme already exists!");
                }
            }
        }

        public void AddUser(Theme theme, User user)
        {
            lock (this)
            {
                if (_themes.ContainsKey(theme))
                {
                    var themeUserContainer = _themes[theme];
                    if (!themeUserContainer.Contains(user))
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
        }

        public IEnumerable<User> GetUsers(Theme theme)
        {
            lock (this)
            {
                if (_themes.ContainsKey(theme))
                {
                    return _themes[theme];
                }
                throw new InvalidOperationException("Theme doesn't exist!");
            }
        }

        public IEnumerable<Theme> GetThemes()
        {
            lock (this)
            {
                return _themes.Keys;
            }
        }

        public Theme GetTheme(string name)
        {
            lock (this)
            {
                return _themes.Keys.SingleOrDefault(p => p.Name.Equals(name));
            }
        }

        public User GetUser(long id)
        {
            lock (this)
            {
                return _themes.Values.SelectMany(allThemeUsers => allThemeUsers).FirstOrDefault(user => user.Id == id);
            }
        }

        public User GetUser(string name)
        {
            lock (this)
            {
                return
                    _themes.Values.SelectMany(allThemeUsers => allThemeUsers).FirstOrDefault(
                        user => user.Name.Equals(name));
            }
        }

        public void RemoveUser(Theme theme, User user)
        {
            lock (this)
            {
                _themes[theme].Remove(user);
            }
        }
    }
}