using JobBoard.Business.Services.Implementations;
using JobBoard.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business
{
    public static class BusinessServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BusinessServiceRegistration));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IJobTypeService, JobTypeService>();
            services.AddScoped<IJobService, JobService>();
            services.AddHostedService<JobExpirationService>();

        }
    }
}
