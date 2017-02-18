using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Models
{
    public class HistoryService
    {
        private IUnitOfWork _db;

        public HistoryService(IUnitOfWork db) { _db = db; }
        
        public IEnumerable<Game> GetUserGame(int? userId)
        {

            IEnumerable<Game> games = _db.Games.Find(g => g.UserID == userId);
            return (userId != null) ? games.Reverse() : null;
        }

    }
}