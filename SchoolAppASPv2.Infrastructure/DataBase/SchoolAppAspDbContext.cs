using Microsoft.EntityFrameworkCore;
using SchoolAppASPv2.Application.Common.Interface;
using SchoolAppASPv2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Infrastructure.DataBase
{
    public class SchoolAppAspDbContext : DbContext, ISchoolAppAspDbContext
    {
        private readonly IDateTimeService _dateTime;
        //public DbSet<AspNetUsers> Users { get; set; }
        
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Image> Images { get; set; }

        public SchoolAppAspDbContext(DbContextOptions<SchoolAppAspDbContext> options,
            IDateTimeService dateTime)
            : base(options)
        {
            _dateTime = dateTime;
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LineItem>().HasNoKey().ToView(null);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

    }
}

// https://aspnetboilerplate.com/Pages/Documents/EF-Core-PostgreSql-Integration