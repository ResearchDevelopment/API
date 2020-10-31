using Microsoft.AspNetCore.Mvc;
using ShadiWebApplication.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShadiWebApplication.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ILogger Logger;
        public BaseController(ILogger logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger), nameof(ControllerBase));
        }
        protected JsonResult JsonResponse(object data)
        {
            return Json(data);
        }
        public string Username
        {
            get
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                var someClaim = claimsIdentity.Claims.Where(c => c.Type.Equals(ClaimTypes.Name));
                if (someClaim != null && someClaim.Any())
                {
                    var content = someClaim.FirstOrDefault().Value;
                    if (!string.IsNullOrEmpty(content))
                        return content;
                }
                return null;
            }
        }
    }
}
