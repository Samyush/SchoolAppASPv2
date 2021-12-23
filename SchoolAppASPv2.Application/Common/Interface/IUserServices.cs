using SchoolAppASPv2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Application.Common.Interface
{
    public interface IUserServices
    {
        AspNetUsers GetUser();

        AspNetUsers GetById(int id);

        AspNetUsers Authenticate(string username, string password);
    }
}
