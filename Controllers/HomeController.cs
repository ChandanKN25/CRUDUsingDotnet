using iLEADWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace iLEADWebAPI.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult EditUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser()
        {
            ViewBag.title = "Delete User";
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditUser(UsersModel usersModel)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44316/api/users ");
                var ResponseTask = client.PutAsJsonAsync("users", usersModel);
                ResponseTask.Wait();
                var result = ResponseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();

                    ModelState.AddModelError(string.Empty, readTask.Result);

                }
                else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError(string.Empty, "User not found");

                }

            }

            return View();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteUser(UsersModel usersModel)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44316/api/users/"+usersModel.user);
                var postTask = client.DeleteAsync(usersModel.user);
                postTask.Wait();
                var result = postTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    ViewBag.result = "User successfully deleted";
                    return View();


                }
                else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError(string.Empty, "User not found or already deleted");

                }


                return View();


            }


        }

        public ActionResult UsersList()
        {
            List<UsersModel> listusersmodel = new List<UsersModel>();

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44316/api/users");
                var postTask = client.GetAsync("users");
                postTask.Wait();
                var result = postTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var userslistTask = result.Content.ReadAsAsync<List<UsersModel>>();
                    userslistTask.Wait();

                    listusersmodel = userslistTask.Result;


                }
                else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {

                    ModelState.AddModelError(string.Empty, result.ReasonPhrase);
                    return View();

                }

                ModelState.AddModelError(string.Empty,"Server error please contact Administrator");
                return View();

            }


        }
    }


}
