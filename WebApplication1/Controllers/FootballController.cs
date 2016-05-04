using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllScoresUI.Services;
using Newtonsoft.Json.Linq;


namespace AllScoresUI.Controllers
{
    public class FootballController : Controller
    {
        // GET: Football
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[ChildActionOnly]
        public PartialViewResult Score()
        {
            return PartialView();
        }

        [HttpGet]
        //[ChildActionOnly]
        public ActionResult Table(int id)
        {
            try 
	        {
                object jsonvalue = AllScoreService.Instance.GetTable(id);
                return PartialView(jsonvalue);
            
		
	        }
            catch (System.Web.Http.HttpResponseException ex)
	        {
                Response.StatusCode = (int)ex.Response.StatusCode;
                return Json(ex.Response.ReasonPhrase, JsonRequestBehavior.AllowGet);		        
	        }            
        }

        [HttpGet]
        public object Leagues()
        {
            string leagues = AllScoreService.Instance.GetLeagues();
            return leagues;
        }

        [HttpGet]
        //[ChildActionOnly]
        public ActionResult Fixtures(int id)
        {
            try
            {
                object jsonvalue = AllScoreService.Instance.GetNextSevenDayFixtures(id);
                return PartialView(jsonvalue);


            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                Response.StatusCode = (int)ex.Response.StatusCode;
                return Json(ex.Response.ReasonPhrase, JsonRequestBehavior.AllowGet);
            }
        }
    }
}