﻿using MyTastyRecipes.models.Models;
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
    public class RecipeService
    {
        public int Create(Recipe model)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Recipe_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@ImageUrl", model.ImageUrl);

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

        public List<AllRecipes> SelectAll()
        {
            List<AllRecipes> allRecipesList = new List<AllRecipes>();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Recipes_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        AllRecipes model = RecipesMapper(reader);
                        allRecipesList.Add(model);
                    }
                }

                conn.Close();
            }

            return allRecipesList;
        }

        public RecipeImageBase SelectById(int id)
        {
            RecipeImageBase model = new RecipeImageBase();

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

        public bool Update(Recipe model)
        {
            bool res = false;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("Recipe_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@ImageUrl", model.ImageUrl);
                    cmd.Parameters.AddWithValue("@Number", model.Number);
                    cmd.Parameters.AddWithValue("@Time", model.Time);
                    cmd.Parameters.AddWithValue("@Yields", model.Yields);
                    cmd.Parameters.AddWithValue("@Instructions", model.Instructions);

                    cmd.ExecuteNonQuery();

                    res = true;
                }

                conn.Close();
            }

            return res;
        }

        private AllRecipes RecipesMapper(SqlDataReader reader)
        {
            AllRecipes model = new AllRecipes();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Name = reader.GetString(index++);
            model.SystemFileName = reader.GetString(index++);

            return model;
        }

        private RecipeImageBase Mapper(SqlDataReader reader)
        {
            RecipeImageBase model = new RecipeImageBase();
            int index = 0;

            model.Name = reader.GetString(index++);
            model.FileId = reader.GetInt32(index++);
            model.SystemFileName = reader.GetString(index++);

            return model;
        }
    }
}
