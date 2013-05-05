using System;
using Hummingbird.Security;

namespace Hummingbird.Data
{
    public interface IDataRow
    {
        bool HasKey();
        Byte[] Version { get; }
        DateTime CreatedDate { get; }
        DateTime LastModifiedDate { get; }
        string CreatedBy { get; }
        string LastModifiedBy { get; }

        void WasModifiedBy(ISecurityContext securityContext);
        void WasCreatedBy(ISecurityContext securityContext);
    }
}
