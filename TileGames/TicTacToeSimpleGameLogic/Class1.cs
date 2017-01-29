using GameLogicPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeSimpleGameLogic
{
    public class Player : IPlayer
    {
        public string PieceKey { get; set; }

        public int PlayerIndex { get; set; }
    }
    public class PlayerLogic : IPlayerLogic
    {
        private List<IPlayer> players = new List<IPlayer>();
        private int CurrentIndex = 0;
        public IPlayer CurrentPlayer => players[CurrentIndex];

        public IEnumerable<IPlayer> Players => players;

        public void AddPlayer(IPlayer player)
        {
            players.Add(player);
        }

        public void NextPlayer()
        {
            CurrentIndex = (CurrentIndex + 1) == players.Count ? 0 : (CurrentIndex + 1);
        }

        public IPlayer PreviousPlayer()
        {
            if (CurrentIndex == 0) return players.Last();
            return players[CurrentIndex - 1];
        }
    }
    public class Class1 : IGameLogic
    {
        private List<IMove<IPlayer>> moves = new List<IMove<IPlayer>>();
        private IPlayerLogic players = new PlayerLogic();
        private bool gameOver = false;
        public int TileHeight => 4;

        public int TileWidth => 4;

        public void AddPlayer(IPlayer player)
        {
            players.AddPlayer(player);
        }

        public BoardState GetBoardState()
        {
            if (moves.Count < 5)
                return BoardState.CONTINUE;
            var m = moves.Where(x => x.Player == players.PreviousPlayer()).ToList();

            if (TileWidth == TileHeight && (
                m.Count(x => x.RowIndex == x.ColumnIndex) == TileWidth ||
                m.Count(x => x.RowIndex + x.ColumnIndex == 2) == TileHeight))
            {
                gameOver = true;
                return BoardState.WON;
            }
            for (int i = 0; i < TileWidth; i++)
            {
                if (m.Count(x => x.RowIndex == i) == TileWidth)
                {
                    gameOver = true;
                    return BoardState.WON;
                }
            }
            for (int i = 0; i < TileHeight; i++)
            {
                if (m.Count(x => x.ColumnIndex == i) == TileHeight)
                {
                    gameOver = true;
                    return BoardState.WON;
                }
            }
            if (moves.Count == 9) { gameOver = true; return BoardState.TIE; }
            return BoardState.CONTINUE;
        }

        public IPlayer GetCurrentPlayer() => players.CurrentPlayer;

        public bool IsMoveValid(IMove<IPlayer> move)
        {
            return !moves.Any(x => x.RowIndex == move.RowIndex && x.ColumnIndex == move.ColumnIndex);
        }

        public bool IsOver() => gameOver;

        public void Move(IMove<IPlayer> move)
        {
            moves.Add(move);
            players.NextPlayer();
        }

        public void Reset()
        {
            gameOver = false;
            moves.Clear();
        }
    }
}
