using System;

namespace CentralService
{
    public class UserDecorator : User
    {
        public IUserCallback Callback { get; set; }

        public UserDecorator(long id, string name, Uri chatService, IUserCallback callback) : base(id, name, chatService)
        {
            Callback = callback;
        }

        public UserDecorator(User user, IUserCallback callback)
            : base(user.Id, user.Name, user.ChatService)
        {
            Callback = callback;
        }
    }
}