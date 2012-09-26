using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Common.Infrastructure.Data
{
    public class DataRow
    {
        public int? Id { get; set; }

        [Timestamp]
        public Byte[] Version { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

    }
}
