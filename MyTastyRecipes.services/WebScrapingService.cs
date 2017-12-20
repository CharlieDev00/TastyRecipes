using HtmlAgilityPack;
using MyTastyRecipes.models.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyTastyRecipes.services
{

    public class WebScrapingService : BaseService
    {
        public List<WebScrapingModel> GetAll()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            List<WebScrapingModel> List = new List<WebScrapingModel>();

            string url = "https://www.tripadvisor.com/Restaurants-g32780-zfg9901-Newport_Beach_California.html";

            var htmlWeb = new HtmlWeb();
            HtmlDocument document = null;
            document = htmlWeb.Load(url);

            //getting all the span with class toptitle first
            var items = document.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("title")).ToList();

            foreach (var node in items)
            {
                WebScrapingModel item = new WebScrapingModel();
                item.Title = node.InnerText;
                //getting the anchor tag inside the span tag
                item.Url = node.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                List.Add(item);
                //var getUrl = document.DocumentNode.Descendants("a");
                // item.Url = node.GetAttributeValue("href", "");
            }
            return List;
        }


        //public int Post(WebScrapingModel model)
        //{
        //    int id = 0;
        //    this.DataProvider.ExecuteNonQuery(
        //        "WebScrapUrl_Insert",
        //        inputParamMapper: delegate (SqlParameterCollection paramCol)
        //        {
        //            paramCol.AddWithValue("@Id", model.Id);
        //            paramCol.AddWithValue("@Title", model.Title);
        //            paramCol.AddWithValue("@Url", model.Url);
        //        },
        //        returnParameters: delegate (SqlParameterCollection paramCol)
        //        {
        //            id = (int)paramCol["@Id"].Value;
        //        }
        //    );
        //    return id;
        //}
    }
}
