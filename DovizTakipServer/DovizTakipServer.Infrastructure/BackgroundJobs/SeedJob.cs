using DovizTakipServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DovizTakipServer.Infrastructure.BackgroundJobs
{
    public sealed class SeedJob
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SeedFirstUser()
        {
            using var scoped = _serviceProvider.CreateScope();
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Taner",
                    LastName = "Saydam",
                    EmailConfirmed = true
                };

                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }
}
