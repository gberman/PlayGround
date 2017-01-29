using System.Collections.Generic;

namespace GameLogicPlugin
{
    public interface IPlayerLogic
    {
        IPlayer CurrentPlayer { get; }
        IEnumerable<IPlayer> Players { get; }
        void AddPlayer(IPlayer player);
        void NextPlayer();
        IPlayer PreviousPlayer();
    }
}