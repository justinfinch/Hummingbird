namespace Hummingbird.Security
{
    public interface ISecurityContext
    {
        string CurrentUser { get; }
    }
}
