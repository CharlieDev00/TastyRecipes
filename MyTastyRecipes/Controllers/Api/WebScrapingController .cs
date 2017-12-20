using MyTastyRecipes.models.Models;
using MyTastyRecipes.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyTastyRecipes.Controllers.Api
{
    [RoutePrefix("api/scraping")]
    public class WebScrapingController : ApiController
    {

        [Route("getall"), HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<WebScrapingModel> res = new List<WebScrapingModel>();
                WebScrapingService svc = new WebScrapingService();

                res = svc.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //[Route("save"), HttpPost]
        //public HttpResponseMessage Post(WebScrapingModel model)
        //{
        //    try
        //    {
        //        WebScrapingService svc = new WebScrapingService();
        //        int id = svc.Post(model);
        //        return Request.CreateResponse(HttpStatusCode.OK, id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}
    }
}