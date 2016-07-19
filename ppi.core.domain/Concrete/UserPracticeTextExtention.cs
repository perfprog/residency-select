using System.Collections.Generic;

namespace PPI.Core.Domain.Concrete
{
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;
    
    public static class UserPracticeTextExtention
    {
        public static IEnumerable<UserPracticeText> GetUserPracticeText(this ISpecialRepository<UserPracticeText> userPracticeText, string HoganId, int language, int report)
        {
            
            
            return userPracticeText.RunQuery("GetPracticeScaleText @HoganId, @Language, @Report", new object[]{
                new System.Data.SqlClient.SqlParameter("HoganId", HoganId),
                new System.Data.SqlClient.SqlParameter("Language",language),
                new System.Data.SqlClient.SqlParameter("Report",report),
            }
                );   
        
        }
    }
}
