namespace GameLogicPlugin
{
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
}
