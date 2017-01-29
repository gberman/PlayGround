using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogicPlugin
{
    public enum BoardState
    {
        WON,
        TIE,
        CONTINUE
    }
    public interface IGameLogic
    {
        int TileWidth { get; }
        int TileHeight { get; }

        BoardState GetBoardState();
        void Move(IMove<IPlayer> move);
        IPlayer GetCurrentPlayer();
        void AddPlayer(IPlayer player);
        void Reset();
        bool IsOver();
        bool IsMoveValid(IMove<IPlayer> move);
    }
    public interface IPlayerLogic
    {
        IPlayer CurrentPlayer { get; }
        IEnumerable<IPlayer> Players { get; }
        void AddPlayer(IPlayer player);
        void NextPlayer();
        IPlayer PreviousPlayer();
    }

    public interface IPlayer
    {
        int PlayerIndex { get; }
        string PieceKey { get; }
    }
    public interface IMove<T> where T : IPlayer
    {
        int RowIndex { get; }
        int ColumnIndex { get; }
        T Player { get; }
    }
}
