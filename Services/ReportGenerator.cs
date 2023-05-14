using DataAccess.Repository;
using Domain;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services
{
    public class ReportGenerator:IReportGenerator
    {

        #region Property  
        private IRepository<Report> _repository;
        #endregion

        #region Constructor  
        public ReportGenerator(IRepository<Report> repository)
        {
            _repository = repository;
        }
        #endregion


        public List<Report> ReportA(Filter filter)
        {
            var res= _repository.GetAllQueryable();
            var finalResult=new List<Report>();

            if (!string.IsNullOrWhiteSpace(filter.DateRange))
            {
                var ss=filter.DateRange.Split(",");
                var date1=Convert.ToDateTime(ss[0]);
                var date2=Convert.ToDateTime(ss[1]);
                res = res.Where(a => a.CreateDateTime >= date1 && a.CreateDateTime <= date2);
            }

            if (!string.IsNullOrWhiteSpace(filter.RequestedParameter))
            {
                finalResult = res.OrderByDescending(a=>a.Id).Select(CreateNewStatement( filter.RequestedParameter )).ToList();
            }
            else
            {
                finalResult = res.OrderByDescending(a=>a.Id).ToList();
            }
            return finalResult;
        }

        public List<Report> ReportB(Filter filter)
        {
            var res= _repository.GetAllQueryable();
            var finalResult=new List<Report>();

            if (string.IsNullOrWhiteSpace(filter.DateRange))
            {
                var ss=filter.DateRange.Split(",");
                var date1=Convert.ToDateTime(ss[0]);
                var date2=Convert.ToDateTime(ss[1]);
                res = res.Where(a => a.CreateDateTime >= date1 && a.CreateDateTime <= date2);
            }

            if (string.IsNullOrWhiteSpace(filter.RequestedParameter))
            {
                finalResult = res.OrderBy(a => a.Id).Select(CreateNewStatement(filter.RequestedParameter)).ToList();
            }
            else
            {
                finalResult = res.OrderBy(a => a.Id).ToList();
            }
            return finalResult;
        }

        public List<Report> ReportC(Filter filter)
        {
            var res= _repository.GetAllQueryable();
            var finalResult=new List<Report>();

            if (string.IsNullOrWhiteSpace(filter.DateRange))
            {
                var ss=filter.DateRange.Split(",");
                var date1=Convert.ToDateTime(ss[0]);
                var date2=Convert.ToDateTime(ss[1]);
                res = res.Where(a => a.CreateDateTime >= date1 && a.CreateDateTime <= date2);
            }

            if (string.IsNullOrWhiteSpace(filter.RequestedParameter))
            {
                finalResult = res.OrderBy(a => a.DeviceId).Select(CreateNewStatement(filter.RequestedParameter)).ToList();
            }
            else
            {
                finalResult = res.OrderBy(a => a.DeviceId).ToList();
            }
            return finalResult;
        }

        public void CreateReport(ReportViewModel model)
        {
            var entity=new Report();
            entity.RxLevel = model.RxLevel;
            entity.CreateDateTime = model.DateTime;
            entity.RSCP = model.RSCP;
            entity.DeviceId = model.DeviceId;
            entity.RSRP = model.RSRP;
            _repository.Insert(entity);
        }


        Func<Report, Report> CreateNewStatement(string fields)
        {
            // input parameter "o"
            var xParameter = Expression.Parameter( typeof( Report ), "o" );

            // new statement "new Data()"
            var xNew = Expression.New( typeof( Report ) );

            // create initializers
            var bindings = fields.Split( ',' ).Select( o => o.Trim() )
        .Select( o => {

            // property "Field1"
            var mi = typeof( Report ).GetProperty( o );

            // original value "o.Field1"
            var xOriginal = Expression.Property( xParameter, mi );

            // set value "Field1 = o.Field1"
            return Expression.Bind( mi, xOriginal );
        }
    );

            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit( xNew, bindings );

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<Report,Report>>( xInit, xParameter );

            // compile to Func<Data, Data>
            return lambda.Compile();
        }




    }
}
