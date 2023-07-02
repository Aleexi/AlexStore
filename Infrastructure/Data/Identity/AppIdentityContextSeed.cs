using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Identity
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            /* If no users in database */ 
            if(!userManager.Users.Any()) 
            {
                /* Create test user */
                var user = new AppUser
                {
                    DisplayName = "Alexander",
                    Email = "alexander@gmail.com",
                    UserName = "alexander@gmail.com",
                    Address = new Address 
                    {
                        FirstName = "Alexander",
                        LastName = "Neumann",
                        Street = "Vretesvägen 4",
                        City = "Östertälje",
                        ZipCode = "15257"
                    }
                };

                await userManager.CreateAsync(user, " P@$$w0RD");
            }
        }
    }
}