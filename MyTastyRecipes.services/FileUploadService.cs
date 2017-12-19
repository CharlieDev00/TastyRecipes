using MyTastyRecipes.models.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web;

namespace MyTastyRecipes.services
{
    public class FileUploadService
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int Insert(UserFile model)
        {
            int id = 0;

            string systemFileName = string.Empty;

            if (model.ByteArray != null)
            {
                systemFileName = string.Format("{0}_{1}{2}",
                    model.UserFileName,
                    Guid.NewGuid().ToString(),
                    model.Extension);

                SaveBytesFile(model.SaveLocation, systemFileName, model.ByteArray);
            }

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Files_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserFileName", model.UserFileName);
                    cmd.Parameters.AddWithValue("@SystemFileName", systemFileName);
                    cmd.Parameters.AddWithValue("@Location", model.SaveLocation);

                    SqlParameter parm = new SqlParameter("@Id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }
            return id;
        }

        private void SavePostedFile(HttpPostedFile postedFile, string location, string systemFileName)
        {
            MemoryStream ms = null;
            string fileBase = "~/images";
            var filePath = HttpContext.Current.Server.MapPath(fileBase + "/" + location + "/" + systemFileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: postedFile.ContentLength, useAsync: true))
            {
                ms = new MemoryStream();
                postedFile.InputStream.CopyTo(ms);
                fs.WriteAsync(ms.ToArray(), 0, postedFile.ContentLength);
            }
        }

        private void SaveBytesFile(string location, string systemFileName, byte[] Bytes)
        {
            string fileBase = "~/images";
            var filePath = HttpContext.Current.Server.MapPath(fileBase + "/" + location + "/" + systemFileName);
            File.WriteAllBytes("C:/repos/github/MyTastyRecipes/MyTastyRecipes/images/Recipe/" + systemFileName, Bytes);
        }

        public void DeleteFile(string filePath, int id)
        {
            //this.DataProvider.ExecuteNonQuery(
            //    "Files_Delete",
            //    inputParamMapper: delegate (SqlParameterCollection paramCol)
            //    {
            //        paramCol.AddWithValue("@Id", id);
            //    }
            //);
            //var deletePath = HttpContext.Current.Server.MapPath("~" + filePath);
            //File.Delete(deletePath);
        }
    }
}

