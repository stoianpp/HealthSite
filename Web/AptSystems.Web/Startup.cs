using AptSystems.Data;
using AptSystems.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(AptSystems.Web.Startup))]
namespace AptSystems.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            Data.ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //var user1 = UserManager.FindByEmail("stoianpp@abv.bg");
            //var result = UserManager.AddToRole(user1.Id, "Admin");


            if (!roleManager.RoleExists("Admin"))
            { 
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);                

                var user = new ApplicationUser();
                user.UserName = "stoian";
                user.Email = "stoianpp@gmail.com";

                string userPWD = "Stoian1!";

                var adminUser = UserManager.Create(user, userPWD);

                if (adminUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }
    
            if (!roleManager.RoleExists("DatabaseAdmin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "DatabaseAdmin";
                roleManager.Create(role);
            
            }
        }
    }
}
