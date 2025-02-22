using JobBoard.DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Services.Implementations
{
    public class JobExpirationService :BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public JobExpirationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var expiredJobs = dbContext.Jobs
                        .Where(j => j.DeadlineDate <= DateTime.Now && !j.IsDeleted)
                        .ToList();

                    if (expiredJobs.Any())
                    {
                        foreach (var job in expiredJobs)
                        {
                            job.IsDeleted = true;
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }

                // Check every hour (3600000 ms = 1 hour)
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
