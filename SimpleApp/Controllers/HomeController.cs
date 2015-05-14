using System;
using System.Linq;
using System.Web.Mvc;
using SimpleApp.Models;
using System.Collections.Generic;

namespace SimpleApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(GetTimeStamps());
        }

        [HttpPost]
        public ActionResult Index(Color color)
        {
            Color? oldColor = Session["color"] as Color?;

            if (oldColor != null)
            {
                Votes.ChangeVote(color, (Color)oldColor);
            }
            else
            {
                Votes.RecordVote(color);
            }
            ViewBag.SelectedColor = Session["color"] = color;
            return View(GetTimeStamps());
        }

        public ActionResult Modules() //new action method called 'Modules' using LINQ to generate an array of Tuple containing strings
        {
            var modules = HttpContext.ApplicationInstance.Modules;
            Tuple<string, string>[] data = modules.AllKeys
                                            .Select(x => new Tuple<string, string>(
                                                x.StartsWith("__Dynamic") ? x.Split('_', ',')[3] : x,
                                                modules[x].GetType().Name))
                                            .OrderBy(x => x.Item1)
                                            .ToArray();
            return View(data);
        }

        private List<string> GetTimeStamps()
        {
            return new List<string>
            {
                string.Format("App timestamp: {0}", HttpContext.Application["app_timestamp"]),
                string.Format("Request timestamp: {0}", Session["request_timestamp"])
            };
        }
    }
}