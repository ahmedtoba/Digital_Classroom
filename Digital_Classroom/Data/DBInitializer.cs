using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Digital_Classroom.Data
{
    public class DBInitializer
    {
        public static async Task CreateRoles(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if(!await roleManager.RoleExistsAsync("Teacher"))
                    await roleManager.CreateAsync(new IdentityRole("Teacher"));
                if (!await roleManager.RoleExistsAsync("Student"))
                    await roleManager.CreateAsync(new IdentityRole("Student"));
            }
        }
    }
}
