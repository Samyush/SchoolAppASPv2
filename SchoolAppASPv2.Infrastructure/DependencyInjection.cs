using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolAppASPv2.Application.Common.Interface;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolAppASPv2.Infrastructure.DataBase;
using SchoolAppASPv2.Infrastructure.Services;

namespace SchoolAppASPv2.Infrastructure
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

            services.AddScoped(provider => (ISchoolAppAspDbContext)provider.GetService<SchoolAppAspDbContext>());
            services.AddHttpContextAccessor();
            services.AddTransient<IUserServices, UserService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<ISchoolEventsService, SchoolEventsServices>();
            return services;
           
        }
    }
}
