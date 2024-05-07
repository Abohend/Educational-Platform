// ignore this file

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
	
	public class TestController : Controller
	{
		public IActionResult Set()
		{
			HttpContext.Session.SetInt32("myvar", 33);
			TempData["name"] = "mohamed";
			CookieOptions cookieOptions = new();
			cookieOptions.Expires = DateTimeOffset.Now.AddHours(1);
			Response.Cookies.Append("age", "23",cookieOptions);
			return Content("Session Set");
		}
		public IActionResult Get()
		{
			int? myint = HttpContext.Session.GetInt32("myvar");
			string? myst = "";
			if (TempData["name"] != null)
				 myst = TempData["name"]!.ToString();
			string age = Request.Cookies["age"] ?? "";

			return Content(myint.ToString() + " " + myst + age);
		}
	}
}
