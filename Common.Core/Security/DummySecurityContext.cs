namespace Common.Core.Security
{
    public class DummySecurityContext : ISecurityContext
    {
        public string CurrentUser
        {
            get { return "Dummy User"; }
        }
    }
}
