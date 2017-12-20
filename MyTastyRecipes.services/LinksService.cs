using MyTastyRecipes.models.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTastyRecipes.services
{
    public class LinksService
    {
        public int Create(LinksModel model)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Links_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Url", model.Url);

                    SqlParameter param = new SqlParameter("@Id", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                    res = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }
            return res;
        }

        public List<LinksModel> SelectAll()
        {
            List<LinksModel> linksList = new List<LinksModel>();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Links_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        LinksModel model = Mapper(reader);
                        linksList.Add(model);
                    }
                }

                conn.Close();
            }

            return linksList;
        }

        public LinksModel SelectById(int id)
        {
            LinksModel model = new LinksModel();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Links_SelectById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        model = Mapper(reader);
                    }
                }

                conn.Close();
            }

            return model;
        }

        public bool Update(LinksModel model)
        {
            bool res = false;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Links_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Url", model.Url);

                    cmd.ExecuteNonQuery();

                    res = true;
                }

                conn.Close();
            }

            return res;
        }

        public bool Delete(int id)
        {
            bool res = false;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Links_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    res = true;
                }

                conn.Close();
            }

            return res;
        }

        private LinksModel Mapper(SqlDataReader reader)
        {
            LinksModel model = new LinksModel();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Title = reader.GetString(index++);
            model.Url = reader.GetString(index++);

            return model;
        }
    }
}
