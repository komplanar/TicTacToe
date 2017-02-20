using System.Linq;

namespace TicTacToe.Models
{
    public class LoginService
    {
        private IUnitOfWork _db;

        public LoginService(IUnitOfWork db) { _db = db; }
        public int Login(User user)
        {
            User newUser = _db.Users.Find(x => x.Name == user.Name).FirstOrDefault() ?? new User { Name = user.Name };
            if (newUser.Id == 0)
            {
                _db.Users.Create(newUser);
                _db.SaveDb();
            }
            return newUser.Id;
        }

    }
}