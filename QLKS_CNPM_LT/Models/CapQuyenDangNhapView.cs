using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace QLKS_CNPM_LT.Models.ViewModel
{
    public class CapQuyenDangNhapView : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            // Implement the logic to retrieve roles for the given username from your database.
            // You can use Entity Framework, ADO.NET, or any other data access method to query your database.
            // Return the roles as an array of strings.
            // Example:
            using (var dbContext = new QLKS_CNPMEntities())
            {
                var userRoles = dbContext.TAIKHOANs
                    .Where(u => u.Gmail == username)
                    .Select(u => u.LOAITK.Trim())
                    .ToArray();
                //Debug.WriteLine($"User roles: {string.Join(",", userRoles)}");
                return userRoles;
            }
        }
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}