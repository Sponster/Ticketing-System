﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ticketing_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tickets()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Projects()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}