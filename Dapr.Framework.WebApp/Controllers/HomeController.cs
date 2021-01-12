using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dapr.Framework.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Event()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Event(Event myEvent)
        {
            var client = new HttpClient();

            var result = await client.PostAsJsonAsync<Event>(@"http://localhost:3500/v1.0/publish/pubsub/filingdata", myEvent);

            return RedirectToAction("/");
        }

        [HttpGet]
        public ActionResult Filing()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Filing(Filing filing)
        {
            var client = new HttpClient();

            //var result = await client.PostAsJsonAsync<Filing>(@"http://localhost:3500/v1.0/publish/pubsub/filingdata", filing);
            var result = await client.PostAsJsonAsync<Filing>(@"http://localhost:5000/filings", filing);

            return RedirectToAction("/");

        }
    }

    public class Event
    {
        public string Name { get; set; }
        public string Payload { get; set; }
    }

    public class Filing
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

}