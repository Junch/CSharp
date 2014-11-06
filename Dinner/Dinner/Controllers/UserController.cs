using Dinner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dinner.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository repository;

        public UserController(IUserRepository repos) {
            this.repository = repos;
        }

        public ViewResult List() {
            return View(repository.Users);
        }
    }
}