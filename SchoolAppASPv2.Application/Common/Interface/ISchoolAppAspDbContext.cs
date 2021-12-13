using Microsoft.EntityFrameworkCore;
using SchoolAppASPv2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Application.Common.Interface
{
    public interface ISchoolAppAspDbContext
    {
        public DbSet<UserPass> Users { get; set; }

        public DbSet<UserPass> UserPass { get; set; }


    }
}
