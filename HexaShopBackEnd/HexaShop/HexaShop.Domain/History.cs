using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class History
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string PreviousValue { get; set; }
        public string NextValue { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ColumnName { get; set; }
        //public int? UserId { get; set; }
        //public string UserFullName { get; set; }
        public string TableName { get; set; }
        public string State { get; set; }
        public string RecordId { get; set; }
    }
}
