using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private GameContext _db;
        private UserSQLRepository _userSQLRepository;
        private GameSQLRepository _gameSQLRepository;
        private TurnSQLRepository _turnSQLRepository;

        public EfUnitOfWork(string nameOrConnectionString)
        {
            _db = new GameContext(nameOrConnectionString);
        }



        public IRepository<Turn> Turns => _turnSQLRepository ?? (_turnSQLRepository = new TurnSQLRepository(_db));
        public IRepository<User> Users => _userSQLRepository ?? (_userSQLRepository = new UserSQLRepository(_db));
        public IRepository<Game> Games => _gameSQLRepository ?? (_gameSQLRepository = new GameSQLRepository(_db));

        public void SaveDb()
        {
            _db.SaveChanges();
        }
    }
}