using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models;
using System.Data.Entity;

namespace TicTacToe.Repositories
{
    public class GameSQLRepository : IRepository<Game>
    {
        private GameContext _db;

        public GameSQLRepository(GameContext db)
        {
            _db = db;
        }

        #region Общение с БД
        public void Create(Game game)
        {
            _db.Games.Add(game);
        }

        public IEnumerable<Game> Find(Func<Game, bool> predicate)
        {
            return _db.Games.Where(predicate).ToList();
        }

        public Game Get(int? id)
        {

            return (id != null) ? _db.Games.Find(id) : null;
        }

        public IEnumerable<Game> GetALL()
        {
            return _db.Games;
        }

        public void Remove(int id)
        {
            var game = _db.Games.Find(id);
            if (game != null)
            {
                _db.Games.Remove(game);
            }
        }

        public void Update(Game game)
        {
            _db.Entry(game).State = EntityState.Modified;
        }
        #endregion
    }
}