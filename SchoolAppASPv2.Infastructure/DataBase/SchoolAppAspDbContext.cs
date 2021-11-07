using Microsoft.EntityFrameworkCore;
using SchoolAppASPv2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Infastructure.DataBase
{
    class SchoolAppAspDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public SchoolAppAspDbContext(DbContextOptions<SchoolAppAspDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
