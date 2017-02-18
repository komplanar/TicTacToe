namespace TicTacToe.Models
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Game> Games { get; }
        IRepository<Turn> Turns { get; }

        void SaveDb();

    }
}
