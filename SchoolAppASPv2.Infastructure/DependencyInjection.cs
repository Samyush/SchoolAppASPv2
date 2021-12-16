using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolAppASPv2.Application.Common.Interface;
using SchoolAppASPv2.Infastructure.DataBase;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolAppASPv2.Infastructure.Services;

namespace SchoolAppASPv2.Infastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("SchoolAppAspConnection");

           services.AddDbContext<SchoolAppAspDbContext>(options =>
           options.UseSqlServer(
                         configuration.GetConnectionString("SchoolAppAspConnection"),
                         b => b.MigrationsAssembly(typeof(SchoolAppAspDbContext).Assembly.FullName)));

            services.AddScoped<ISchoolAppAspDbContext>(provider => (ISchoolAppAspDbContext)provider.GetService<SchoolAppAspDbContext>());
            services.AddHttpContextAccessor();
            services.AddTransient<IUserServices, UserService>();
            services.AddTransient<IDateTimeService, DateTimeService>();

            return services;
           
        }
    }
}
