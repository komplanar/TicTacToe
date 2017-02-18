using System;
using System.Collections.Generic;

namespace TicTacToe.Models
{
    public class Game
    {
        public enum State
        {
            NotFinish, UserWin, UserLose, Draw
        }
        public int Id { get; set; }
        public int UserID { get; set; }

        public DateTime DateOfStart { get; set; }
        public State Result { get; set; }
        public virtual ICollection<Turn> Turns { get; set; }

        public Game()
        {
            Turns = new List<Turn>();
            DateOfStart = DateTime.Now;
        }
        public Game(User user)
        {
            Turns = new List<Turn>();
            DateOfStart = DateTime.Now;
            UserID = user.Id;
        }

    }
}