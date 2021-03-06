﻿using System.Web.Mvc;
using Common;

namespace FrontEnd.Controllers
{
    [Authorize (Roles = RoleNames.Teacher)]
    public class InstructorController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCourse()
        {
            return View();
        }

        public ActionResult Course(int id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save()
        {
            return Json(null);
        }
    }
}