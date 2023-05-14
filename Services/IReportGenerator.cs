using Domain;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IReportGenerator
    {
        List<Report> ReportA(Filter filter);

        List<Report> ReportB(Filter filter);

        List<Report> ReportC(Filter filter);

        void CreateReport(ReportViewModel model);

    }
}
