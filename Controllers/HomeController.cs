using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ShadiWebApplication.Logger;
using ShadiWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;



namespace ShadiWebApplication.Controllers
{
    public class HomeController : BaseController
    {
        private IConfiguration configuration;

        public HomeController(IConfiguration iConfig, ILogger logger) : base(logger)
        {
            configuration = iConfig;
        }

        [Authorize]
        public IActionResult Index()
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;

            Logger.Info("Login action Home/Index", claimsIdentity);
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
