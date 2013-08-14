using System;

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

        void WasModifiedBy();
        void WasCreatedBy();
    }
}
