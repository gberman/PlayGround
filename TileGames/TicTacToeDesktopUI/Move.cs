using GameLogicPlugin;

namespace TicTacToeDesktopUI
{
    public class Move : IMove<IPlayer>
    {
        public int ColumnIndex { get; set; }

        public IPlayer Player { get; set; }

        public int RowIndex { get; set; }
    }
}