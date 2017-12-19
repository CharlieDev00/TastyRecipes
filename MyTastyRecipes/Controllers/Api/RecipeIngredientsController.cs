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
    [RoutePrefix("api/ingredients")]
    public class RecipeIngredientsController : ApiController
    {
        [Route("create"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Create(RecipeIngredients model)
        {
            try
            {
                RecipeIngredientsService svc = new RecipeIngredientsService();
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
            List<RecipeIngredients> res = new List<RecipeIngredients>();
            RecipeIngredientsService svc = new RecipeIngredientsService();

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
            RecipeIngredients res = new RecipeIngredients();
            RecipeIngredientsService svc = new RecipeIngredientsService();

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
        public HttpResponseMessage Update(RecipeIngredients model)
        {
            bool res = false;
            RecipeIngredientsService svc = new RecipeIngredientsService();

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
            RecipeIngredientsService svc = new RecipeIngredientsService();

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