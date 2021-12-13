using Microsoft.AspNetCore.Http;
using SchoolAppASPv2.Application.Common;
using SchoolAppASPv2.Application.Common.Interface;
using SchoolAppASPv2.Core.Entities;
using SchoolAppASPv2.Infastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Infastructure.Services
{
    public class UserService : IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor, ISchoolAppAspDbContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId { get { return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimsIdentity.DefaultNameClaimType); } }

        private ISchoolAppAspDbContext _context;

        public UserPass Authenticate(string username, string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.UserPass.SingleOrDefault(x => x.Name == username);

            // check if username exists
            if (user == null)
                return null;

            var hash = password.CalculateMD5Hash();
            if (user.Password != hash)
            {
                return null;

            }

            // authentication successful
            return user;
        }


        public UserPass GetById(int id)
        {
            return _context.UserPass.Find(id);
        }

        public UserPass GetUser()
        {
            var userId = int.Parse(UserId);
            var user = GetById(userId);

            // authentication success
            return user;
        }
    }
}
