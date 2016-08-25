using GogoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ViaGoGo.CD.Models;
using ViaGoGo.CD.Services;

namespace ViaGoGo.CD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string searchText)
        {
            ViewBag.Message = "ViaGoGo Search Page";
            var viaGoGoService = new ViaGoGoRESTService();
            var viaGoGoClient = await viaGoGoService.authenticateViaGoGo();

            var actions = new Actions();
            var results = await actions.getSearchResults(viaGoGoClient, searchText);

            if(results.Count == 0)
            {
                return Json("{ \"data\": { \"results\" : \"0\", \"status\" : \"No results found\"} }");
            }
            Event evnt = new Event();
            var events = evnt.setEventList(results);
            evnt.setLowestCountryPrice(events);

            var json = new JavaScriptSerializer().Serialize(events);

            return Json(json);
        }

        [HttpPost]
        public async Task<ActionResult> Listings(int eventId)
        {
            var viaGoGoService = new ViaGoGoRESTService();
            var viaGoGoClient = await viaGoGoService.authenticateViaGoGo();

            var actions = new Actions();
            var results = await actions.getListings(viaGoGoClient, eventId);
            var json = new JavaScriptSerializer().Serialize(results);

            return Json(json);
        }

        [HttpPost]
        public async Task<ActionResult> Pagination(string paginationURL)
        {
            var viaGoGoService = new ViaGoGoRESTService();
            var viaGoGoClient = await viaGoGoService.authenticateViaGoGo();
            var actions = new Actions();
            var results = await actions.listingPagination(viaGoGoClient, paginationURL);
            var json = new JavaScriptSerializer().Serialize(results);

            return Json(json);
        }
    }
}

