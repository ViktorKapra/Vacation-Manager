using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data;

namespace VacationManager.Helpers
{
    public static class UserCredentialsHelper
    {
        public static int FindUserId(VacationManagerContext _context,ClaimsPrincipal User)
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            int userId = _context.Users.FirstOrDefault(u => u.UserName == userEmail).Id;
            return userId;
        }

        public static string FindUserRole(VacationManagerContext _context, ClaimsPrincipal User)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            string userRole = _context.Users.FirstOrDefault(u => u.UserName == userEmail).Role.Name;
            return userRole;
        }
    }
}
