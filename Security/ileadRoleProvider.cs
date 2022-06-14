using iLEADWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Security;

namespace iLEADWebAPI.Security
{
    public class ileadRoleProvider : RoleProvider
    {
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

        public override string[] GetRolesForUser(string username)
        {
            string[] userroles = { "" };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44316/api/users/" + username);

                var postTask = client.GetAsync(username);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UsersModel>();
                    readTask.Wait();

                    var dbUserDetails = readTask.Result;
                    if (dbUserDetails.designation != null)
                    {
                        userroles[0] = dbUserDetails.designation;
                    }
                    //return RedirectToAction("Index");
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    userroles[0] = "NO Roles Assigned to this user";

                }
                return userroles;
            }
        
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