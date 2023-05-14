using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Report:BaseEntity
    {
        //public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int DeviceId { get; set; }
        public decimal RxLevel { get; set; }
        public decimal RSCP { get; set; }
        public decimal RSRP { get; set; } 
    }
}
