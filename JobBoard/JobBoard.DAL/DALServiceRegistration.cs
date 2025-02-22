using JobBoard.DAL.Repositories.Implementations;
using JobBoard.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.DAL
{
    public static class DALServiceRegistration
    {
        public static void AddDALServices(this IServiceCollection services) 
        {
            services.AddScoped<ICategoryRepo,CategoryRepo>();
            services.AddScoped<IEducationRepo,EducationRepo>();
            services.AddScoped<IGenderRepo,GenderRepo>();
            services.AddScoped<ISkillRepo,SkillRepo>();
            services.AddScoped<IJobTypeRepo,JobTypeRepo>();
            services.AddScoped<IJobRepo,JobRepo>();
        }
    }
}
