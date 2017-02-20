using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models;
using System.Data.Entity;

namespace TicTacToe.Repositories
{
    public class TurnSQLRepository : IRepository<Turn>
    {
        private GameContext _db;

        public TurnSQLRepository(GameContext db)
        {
            _db = db;
        }

        #region Общение с БД
        public void Create(Turn turn)
        {
            _db.Turns.Add(turn);
        }

        public IEnumerable<Turn> Find(Func<Turn, bool> predicate)
        {
            return _db.Turns.Where(predicate).ToList();
        }

        public Turn Get(int? id)
        {
            return (id != null) ? _db.Turns.Find(id) : null;
        }

        public IEnumerable<Turn> GetALL()
        {
            return _db.Turns;
        }

        public void Remove(int id)
        {
            var turn = _db.Turns.Find(id);
            if (turn != null)
            {
                _db.Turns.Remove(turn);
            }
        }

        public void Update(Turn turn)
        {
            _db.Entry(turn).State = EntityState.Modified;
        }
        #endregion
    }
}