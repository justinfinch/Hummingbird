using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Infrastructure.Data
{
    public interface IPersistable<T> 
        where T : DataRow
    {
        void Load(T dataRow);
        T ExtractData();
    }
}
