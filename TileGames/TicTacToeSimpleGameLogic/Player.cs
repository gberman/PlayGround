using GameLogicPlugin;

namespace TicTacToeSimpleGameLogic
{
    public class Player : IPlayer
    {
        public string PieceKey { get; set; }

        public int PlayerIndex { get; set; }
    }
}
