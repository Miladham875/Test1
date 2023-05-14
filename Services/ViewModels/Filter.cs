using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class Filter
    {
        public string ReportType { get; set; }
        //must be seperated by comma
        public string DateRange { get; set; }
        //must be seperated by comma
        public string RequestedParameter { get; set; }
        public string OutputType { get; set; }

    }
}
