namespace Hummingbird.Data
{
    public interface IObjectWithState
    {
        ObjectState CurrentObjectState { get; }
    }
}
