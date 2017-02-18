using System;
using System.Linq;

namespace TicTacToe.Models
{
    public class GameService
    {
        public bool IsPlayerFirst { get; }
        private IUnitOfWork _db;
        public GameLogic gameLogic;

        public GameService(IUnitOfWork db)
        {
            IsPlayerFirst = new Random().Next(2) == 1 ? true : false;
            gameLogic = new GameLogic();
            _db = db;
        }

        public void FillField(Game game)
        {
            gameLogic = new GameLogic();
            foreach (var turn in game.Turns)
            {
                gameLogic.CellState[turn.Cell] = turn.IsPlayer ? CellValue.USER : CellValue.BOT;
            }
        }
        public int NewGame(int? userId)
        {
            var user = _db.Users.Get(userId);

            if (user != null)
            {
                var game = new Game(user);
                _db.Games.Create(game);
                _db.SaveDb();

                gameLogic = new GameLogic();
                return game.Id;
            }
            return 0;
        }
        public void IfBotMoveFirst(int? gameId)
        {
            if (!IsPlayerFirst)
            {
                var game = _db.Games.Get(gameId);
                if (!game.Turns.Any())
                {
                    var turn = new Turn(false, gameLogic.BotMove());
                    game.Turns.Add(turn);
                    _db.Games.Update(game);
                    _db.SaveDb();
                }
            }
        }
        public Game.State Move(int? gameId, int position)
        {
            var game = _db.Games.Get(gameId);

            if (game.Turns.Any())
            {
                FillField(game);
            }


            //var availableMoves = gameLogic.GetAvailableMoves();
            //int cell = availableMoves[new Random().Next(availableMoves.Count)];
            if (!gameLogic.UserMove(position))
            {
                return Game.State.NotFinish;
            }
            var turn = new Turn(true, position);
            game.Turns.Add(turn);

            Game.State check = gameLogic.CheckGameState();

            if (check != Game.State.NotFinish)
            {
                game.Result = check;
                _db.Games.Update(game);
                _db.SaveDb();
                return check;
            }

            turn = new Turn(false, gameLogic.BotMove());
            game.Turns.Add(turn);

            check = gameLogic.CheckGameState();
            if (check != Game.State.NotFinish)
            {
                game.Result = check;
                _db.Games.Update(game);
                _db.SaveDb();
                return check;
            }
            _db.Games.Update(game);
            _db.SaveDb();
            return Game.State.NotFinish;
        }



    }
}