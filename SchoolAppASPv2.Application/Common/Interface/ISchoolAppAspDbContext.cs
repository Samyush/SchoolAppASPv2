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
        //public DbSet<AspNetUsers> Users { get; set; }

        public DbSet<AspNetUsers> AspNetUsers { get; set; }


    }
}
