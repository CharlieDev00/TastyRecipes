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
    [RoutePrefix("api/links")]
    public class LinksController : ApiController
    {
        [Route("save"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Post(LinksModel model)
        {
            try
            {
                LinksService svc = new LinksService();
                int res = svc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, res);

            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [Route("getall"), HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectAllIngredients()
        {
            List<LinksModel> res = new List<LinksModel>();
            LinksService svc = new LinksService();

            try
            {
                res = svc.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("get/{id:int}"), HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectById(int id)
        {
            LinksModel res = new LinksModel();
            LinksService svc = new LinksService();

            try
            {
                res = svc.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut, AllowAnonymous, Route("update")]
        public HttpResponseMessage Update(LinksModel model)
        {
            bool res = false;
            LinksService svc = new LinksService();

            try
            {
                res = svc.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete, AllowAnonymous, Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            bool res = false;
            LinksService svc = new LinksService();

            try
            {
                res = svc.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}