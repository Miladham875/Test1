using System;

namespace Services.ViewModels
{
    public class ReportViewModel
    {
        public int DeviceId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal RxLevel { get; set; }
        public decimal RSCP { get; set; }
        public decimal RSRP { get; set; }

    }
}
