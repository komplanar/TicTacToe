using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models;
using System.Data.Entity;

namespace TicTacToe.Repositories
{
    public class UserSQLRepository : IRepository<User>
    {
        private GameContext _db;

        public UserSQLRepository(GameContext db)
        {
            _db = db;
        }

        #region Общение с БД
        public void Create(User user)
        {
            _db.Users.Add(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }

        public User Get(int? id)
        {
            return (id != null) ? _db.Users.Find(id) : null;
        }

        public IEnumerable<User> GetALL()
        {
            return _db.Users;
        }

        public void Remove(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
            }

        }

        public void Update(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
        }
        #endregion
    }
}