using MyTastyRecipes.models.Models;
using MyTastyRecipes.models.ViewModel;
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
    public class RecipeIngredientsService
    {
        public int Create(RecipeIngredients model)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("RecipeIngredients_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Number", model.Number);
                    cmd.Parameters.AddWithValue("@Measurements", model.Measurements);
                    cmd.Parameters.AddWithValue("@Ingredient", model.Ingredient);
                    cmd.Parameters.AddWithValue("@RecipeId", model.RecipeId);


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

        public List<RecipeIngredients> SelectAll()
        {
            List<RecipeIngredients> recipeIngredientsList = new List<RecipeIngredients>();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("RecipeIngredients_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        RecipeIngredients model = Mapper(reader);
                        recipeIngredientsList.Add(model);
                    }
                }

                conn.Close();
            }

            return recipeIngredientsList;
        }

        public RecipeIngredients SelectById(int id)
        {
            RecipeIngredients model = new RecipeIngredients();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("RecipeIngredients_SelectById", conn))
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

        public bool Update(RecipeIngredients model)
        {
            bool res = false;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("RecipeIngredients_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Number", model.Number);
                    cmd.Parameters.AddWithValue("@Measurements", model.Measurements);
                    cmd.Parameters.AddWithValue("@Ingredient", model.Ingredient);
                    cmd.Parameters.AddWithValue("@RecipeId", model.RecipeId);

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

                using (SqlCommand cmd = new SqlCommand("dbo.RecipeIngredients_Delete", conn))
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

        private RecipeIngredients Mapper(SqlDataReader reader)
        {
            RecipeIngredients model = new RecipeIngredients();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Number = reader.GetInt32(index++);
            model.Measurements = reader.GetString(index++);
            model.Ingredient = reader.GetString(index++);
            model.RecipeId = reader.GetInt32(index++);

            return model;
        }
    }
}
