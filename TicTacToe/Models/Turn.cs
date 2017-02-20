namespace TicTacToe.Models
{
    public class Turn
    {
        public int Id { get; set; }
        public bool IsPlayer { get; set; }
        public int GameID { get; set; }
        public int Cell { get; set; }

        public Turn()
        {

        }

        public Turn(bool isPlayer, int cell)
        {
            IsPlayer = isPlayer;
            Cell = cell;
        }

    }
}