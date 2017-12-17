using MyTastyRecipes.Models;
using MyTastyRecipes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyTastyRecipes.Controllers.Api
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [Route("create"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Post(NewUser model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                UserService svc = new UserService();
                int res = svc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, res);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("login"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Login(NewUser model)
        {

            bool res = false;
            UserService svc = new UserService();

            try
            {
                res = svc.Login(model);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}