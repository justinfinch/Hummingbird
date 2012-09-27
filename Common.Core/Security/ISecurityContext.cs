namespace Common.Core.Security
{
    public interface ISecurityContext
    {
        string CurrentUser { get; }
    }
}
