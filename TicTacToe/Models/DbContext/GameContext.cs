using System.Data.Entity;

namespace TicTacToe.Models
{
    public class GameContext : DbContext
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Turn> Turns { get; set; }

        public GameContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<GameContext>(new DbInitializer());
        }
    }

    public class DbInitializer : DropCreateDatabaseIfModelChanges<GameContext>
    {
        protected override void Seed(GameContext context)
        {
            base.Seed(context);
        }
    }
}