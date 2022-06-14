using iLEADWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace iLEADWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public List<UsersModel> Get()
        {
            iLead2021Entities3 ilead = new iLead2021Entities3();
            var results = from users in ilead.TestUserTables
                          select users;
            List<UsersModel> iUsers = new List<UsersModel>();

            foreach (var userObj in results)
            {
                iUsers.Add(new UsersModel { user = userObj.username, password = userObj.password/*,designation = userObj.designation*/ });

            }
            return iUsers;
            //List<UsersModel> usersModel = new List<UsersModel>();

            //string connstring = "Data Source=IN-306V4B3;Initial Catalog=iLead2021;Persist Security Info=True;User ID=sa;Password=sa";

            //SqlConnection conn = new SqlConnection(connstring);

            //SqlCommand command;
            //SqlDataReader dataReader;
            //string sqlstring = "select * from iusers  ";
            ////string dbUser = string.Empty;
            ////string dbPass = string.Empty;
            //conn.Open();
            //command = new SqlCommand(sqlstring, conn);

            //dataReader = command.ExecuteReader();

            //while (dataReader.Read())
            //{
            //    //dbUser = dataReader.GetString(0);
            //    //dbPass = dataReader.GetString(1);
            //    usersModel.Add(new UsersModel { user = dataReader.GetString(0), password = dataReader.GetString(1) });
            //}
            //return usersModel;
        }

        // GET api/users/5
        public UsersModel Get(string username)
        {

            iLead2021Entities3 ilead = new iLead2021Entities3();
            var results = from users in ilead.TestUserTables
                          where users.username == username
                          select users;
            UsersModel iUser = new UsersModel();

            foreach (var userObj in results)
            {
                iUser.user = userObj.username;
                iUser.password = userObj.password;
                iUser.designation = userObj.designation;
            }
            return iUser;

            //UsersModel usersModel = new UsersModel();

            //string connstring = "Data Source=IN-306V4B3;Initial Catalog=iLead2021;Persist Security Info=True;User ID=sa;Password=sa";


            //SqlConnection conn = new SqlConnection(connstring);

            //SqlCommand command;
            //SqlDataReader dataReader;
            //string sqlstring = "select * from iusers where username = '" + username + "'";
            ////string dbUser = string.Empty;
            ////string dbPass = string.Empty;
            //conn.Open();
            //command = new SqlCommand(sqlstring, conn);

            //dataReader = command.ExecuteReader();

            //while (dataReader.Read())
            //{
            //    //dbUser = dataReader.GetString(0);
            //    //dbPass = dataReader.GetString(1);
            //    usersModel.user = dataReader.GetString(0);
            //    usersModel.password = dataReader.GetString(1);
            //}
            //return usersModel;
        }

        // POST api/values
        public IHttpActionResult Post(UsersModel usersModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a Valid Model");
            iLead2021Entities3 ilead = new iLead2021Entities3();
            var existingUser = ilead.TestUserTables.Where(u => u.username == usersModel.user).FirstOrDefault();

            if (existingUser != null)
                return BadRequest("User already exist!");

            ilead.TestUserTables.Add(new TestUserTable { username = usersModel.user, password = usersModel.password, designation = usersModel.designation });
            ilead.SaveChanges();

            return Ok("user successfully created");

        }

        // PUT api/values/5
        public IHttpActionResult Put(UsersModel usersModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

                  var ctx = new iLead2021Entities3();
            
                var existingUser = ctx.TestUserTables.Where(u => u.username == usersModel.user).FirstOrDefault();

                if (existingUser != null)
                {
                    if (!string.IsNullOrEmpty(usersModel.password))
                        existingUser.password = usersModel.password;


                    ctx.SaveChanges();



                }
                else
                {

                    return NotFound();
                }

            

            return Ok("save changes successfully");


        }

        // DELETE api/values/5
        public IHttpActionResult Delete(string username)
        {
            if (!ModelState.IsValid)
                return BadRequest("not a valid model");
            iLead2021Entities3 ilead = new iLead2021Entities3();
            var existingUser = ilead.TestUserTables.Where(u => u.username == username).FirstOrDefault();
            if(existingUser!= null)
            {
                ilead.TestUserTables.Remove(existingUser);
                ilead.SaveChanges();

            }
            else
            {
                return NotFound();
            }

            return Ok();

        }

    }
}
