﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VRM.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        //public ActionResult Details()
        //{
        //    return View();
        //}
    }
}