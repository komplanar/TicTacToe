using System.Linq;

namespace TicTacToe.Models
{
    public class LoginService
    {
        private IUnitOfWork _db;

        public LoginService(IUnitOfWork db) { _db = db; }
        public int Login(User user)
        {
            User usr = _db.Users.Find(x => x.Name == user.Name).FirstOrDefault() ?? new User { Name = user.Name };
            if (usr.Id == 0)
            {
                _db.Users.Create(usr);
                _db.SaveDb();
            }
            return usr.Id;
        }

    }
}