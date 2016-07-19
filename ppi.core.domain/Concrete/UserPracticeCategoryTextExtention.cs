using System.Collections.Generic;


namespace PPI.Core.Domain.Concrete
{
    
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;
    public static class UserPracticeCategoryTextExtention
    {
        public static IEnumerable<UserPracticeCategoryText> GetUserPracticeCategoryText(this ISpecialRepository<UserPracticeCategoryText> userPracticeCategoryText, string HoganId, int language, int report, int programId)
        {


            return userPracticeCategoryText.RunQuery("GetPracticeCategoryText @HoganId, @Language, @Report, @ProgramId", new object[]{
                new System.Data.SqlClient.SqlParameter("HoganId", HoganId),
                new System.Data.SqlClient.SqlParameter("Language",language),
                new System.Data.SqlClient.SqlParameter("Report",report),
                new System.Data.SqlClient.SqlParameter("ProgramId",programId)
            }
                );

        }
    }
}
