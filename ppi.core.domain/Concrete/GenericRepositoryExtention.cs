using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Core.Domain.Concrete
{
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;
    public static class GenericRepositoryExtention
    {
        public static String ClonePraciceReports(this ISpecialRepository<String> clonePracticeReports, int report, int orginalProgramId, int newProgramId)
        {
            var execute = clonePracticeReports.ExecuteQuery("ClonePracticeScaleReport @report, @orginalProgramId, @newProgramId, @newText", new object[]
            {
                new System.Data.SqlClient.SqlParameter("report", report),
                new System.Data.SqlClient.SqlParameter("orginalProgramId",orginalProgramId),
                new System.Data.SqlClient.SqlParameter("newProgramId",newProgramId),
                new System.Data.SqlClient.SqlParameter("newText",false)
            });
            return "True";        
        }
    }
}
