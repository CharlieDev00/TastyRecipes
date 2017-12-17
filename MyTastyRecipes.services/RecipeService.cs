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
    public class RecipeService
    {
        public int Create(Recipe model)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.User_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Name);
                    cmd.Parameters.AddWithValue("@Password", model.ImageUrl);

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

        public Recipe SelectById(int id)
        {
            Recipe model = new Recipe();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Recipe_SelectById_Create", conn))
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

        private Recipe Mapper(SqlDataReader reader)
        {
            Recipe model = new Recipe();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Name = reader.GetString(index++);
            model.ImageUrl = reader.GetInt32(index++);
            return model;
        }
    }
}
