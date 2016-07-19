using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PPI.Core.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {        
        protected override void Seed(ApplicationDbContext context)
        {
            //var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
            //string name = "Admin";
            //string password = "test123";

            ////Create Role Admin if it does not exist
            //if (!RoleManager.RoleExists(name))
            //{
            //    var roleresult = RoleManager.Create(new IdentityRole(name));
            //}

            //var AdminUser = new ApplicationUser();
            //AdminUser.UserName = name;
            //var result = UserManager.Create(AdminUser,password);
            //if (result.Succeeded)
            //{
            //    var roleresult = UserManager.AddToRole(AdminUser.Id,name);
            //}


            //base.Seed(context);            
        }
    }

}