using GameLogicPlugin;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToeSimpleGameLogic
{
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
}
