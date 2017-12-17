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
    [RoutePrefix("api/recipe")]
    public class RecipeController : ApiController
    {

        [Route("create"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Post(Recipe model)
        {
            try
            {
                RecipeService svc = new RecipeService();
                int res = svc.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK, res);

            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}