using MyTastyRecipes.models.Models;
using MyTastyRecipes.models.ViewModel;
using MyTastyRecipes.services;
using System;
using System.Collections.Generic;
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

        [Route("fileUpload"), HttpPost, AllowAnonymous]
        public HttpResponseMessage FilePost(EncodedImage encodedImage)
        {
            try
            {
                FileUploadService svc = new FileUploadService();
                byte[] newBytes = Convert.FromBase64String(encodedImage.EncodedImageFile);
                UserFile model = new UserFile();
                model.UserFileName = "recipe";
                model.ByteArray = newBytes;
                model.Extension = encodedImage.FileExtension;
                model.SaveLocation = "Recipe";

                int resp = svc.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("getall"), HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectAllRecipes()
        {
            List<AllRecipes> res = new List<AllRecipes>();
            RecipeService svc = new RecipeService();

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

        [Route("getBaseRecipe/{id:int}"), HttpGet, AllowAnonymous]
        public HttpResponseMessage GetBase(int id)
        {
            RecipeImageBase model = new RecipeImageBase();
            RecipeService svc = new RecipeService();
            try
            {
                model = svc.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut, AllowAnonymous, Route("update")]
        public HttpResponseMessage Update(Recipe model)
        {
            bool res = false;
            RecipeService svc = new RecipeService();

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
    }
}