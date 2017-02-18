using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Models
{
    public enum CellValue { EMPTY, USER, BOT }
    public class GameLogic
    {
        public Dictionary<int, CellValue> CellState { get; set; }

        public GameLogic()
        {
            CellState = new Dictionary<int, CellValue>();
            for (int i = 0; i < 9; i++)
            {
                CellState.Add(i, CellValue.EMPTY);
            }
        }

        public bool UserMove(int cellNumber)
        {
            if (cellNumber > 8 || cellNumber < 0)
            {
                throw new ArgumentOutOfRangeException("cellNumber", "Не правильно пронумированы ячейки поля (0-8)");
            }
            if (CellState[cellNumber] == CellValue.EMPTY)
            {
                CellState[cellNumber] = CellValue.USER;
                return true;
            }
            return false;
        }
        /*поменять реализацию,
                                пока рандом*/
        public int BotMove2()
        {
            List<int> availableMoves = GetAvailableMoves();
            int res = availableMoves[new Random().Next(availableMoves.Count())];
            CellState[res] = CellValue.BOT;
            return res;
        }

        public List<int> GetAvailableMoves()
        {
            return CellState.Where(x => x.Value == CellValue.EMPTY).Select(x => x.Key).ToList();
        }

        public Game.State CheckGameState()
        {
            int[,] posLine = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 },
                { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            for (int i = 0; i < 8; i++)
            {
                if (CellState[posLine[i, 0]] == CellState[posLine[i, 1]] && CellState[posLine[i, 1]] == CellState[posLine[i, 2]]
                    && CellState[posLine[i, 0]] != CellValue.EMPTY)
                {
                    return (Game.State)CellState[posLine[i, 0]];
                }
            }
            if (GetAvailableMoves().Count != 0)
            {
                return (Game.State)0;
            }
            return (Game.State)3;
        }

        public int BotMove()
        {
            int result;
            List<int> availableMoves = GetAvailableMoves();
            Dictionary<int, int> weights = new Dictionary<int, int>();

            foreach (var i in availableMoves)
            {
                weights.Add(i, ComputeCellWeight(i));
            }
            result = weights.OrderByDescending(x => x.Value).First().Key;
            CellState[result] = CellValue.BOT;
            return result;
        }
        public int ComputeCellWeight(int cell)
        {
            int weight = 0;
            int[,] posLine = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 },
                { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            int sum;
            for (int i = 0; i < posLine.Length; i++)
            {
                if (cell == posLine[i / 3, i % 3])
                {
                    sum = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        sum += (int)CellState[posLine[i / 3, j]];
                    }
                    switch (sum)
                    {
                        case 3: weight += 0;
                            break;
                        case 0:
                        case 1:
                            weight += 1;
                            break;
                        case 4: weight += 85;
                            break;
                        case 2:
                            int max = 0;
                            for (int j = 0; j < 3; j++)
                            {
                                var temp = (int)CellState[posLine[i / 3, j]];
                                if (temp > max) max = temp;
                            }
                            if (max == 2) weight += 5;
                            else weight += 21;
                            break;
                        default:
                            break;
                    }

                }
            }
                return weight;
        }
    }
}