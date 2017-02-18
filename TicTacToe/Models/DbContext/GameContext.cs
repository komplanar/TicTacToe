using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class GameContext : DbContext
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Turn> Turns { get; set; }

        public GameContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
    }

    /*public class DbInitializer : DropCreateDatabaseAlways<GameContext>
    {
        protected override void Seed(GameContext context)
        {
            context.Users.Add(new User { Name = "TestUser"});
            base.Seed(context);
        }
    }*/
}