using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Web.Types;

namespace Web.Factory
{
    public class UserSeedFactory
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            context.Database.EnsureCreated();

            var password = "StfuN00b!";
            var userName = "davidmerk25@gmail.com";
            foreach (string role in Enum.GetNames(typeof(RoleType)))
                if (await roleManager.FindByNameAsync(role) == null)
                    await roleManager.CreateAsync(new Role(role, $"This is the {role} role.", DateTime.Now));
            if (await userManager.FindByNameAsync("davidmerk25@gmail.com") == null) {
                var user = new User
                {
                    UserName = userName,
                    Email = userName,
                    Profile = new Profile
                    {
                        UserName = userName
                    },
                    Role = await roleManager.FindByNameAsync(RoleType.Admin.ToString())
                };
                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, RoleType.Admin.ToString());
                    context.Set<Profile>().FirstOrDefault(u => u.UserId == Guid.Empty).UserId = user.Id;
                    context.SaveChanges();
                }
            }
        }
    }
}
