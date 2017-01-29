namespace GameLogicPlugin
{
    public interface IMove<T> where T : IPlayer
    {
        int RowIndex { get; }
        int ColumnIndex { get; }
        T Player { get; }
    }
}
