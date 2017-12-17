using MyTastyRecipes.Models;
using MyTastyRecipes.services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MyTastyRecipes.Services
{
    public class UserService
    {
        

        public int Create(NewUser userModel)
        {
            int userId = 0;

            CryptographyService cryptSvc = new CryptographyService();
            userModel.Salt = cryptSvc.GenerateRandomString(15);
            userModel.EncryptedPass = cryptSvc.Hash(userModel.Password, userModel.Salt);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.User_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", userModel.Email);
                    cmd.Parameters.AddWithValue("@Password", userModel.EncryptedPass);
                    cmd.Parameters.AddWithValue("@Salt", userModel.Salt);

                    SqlParameter param = new SqlParameter("@Id", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                    userId = (int)cmd.Parameters["@Id"].Value; 
                }
                conn.Close();
            }
            return userId;
        }

        public bool Login(NewUser userModel)
        {
            bool user = false;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.User_GetByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", userModel.Email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        NewUser responseModel = Mapper(reader);

                        int multOf4 = responseModel.Salt.Length % 4;
                        if (multOf4 > 0)
                        {
                            responseModel.Salt += new string('=', 4 - multOf4);
                        }
                        CryptographyService cryptSvc = new CryptographyService();
                        string passwordHash = cryptSvc.Hash(userModel.Password, responseModel.Salt);

                        if (passwordHash == responseModel.EncryptedPass)
                        {
                            user = true;
                        }
                    }
                }

                conn.Close();
            }

            return user;
        }

        private NewUser Mapper(SqlDataReader reader)
        {
            NewUser model = new NewUser();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Email = reader.GetString(index++);
            model.EncryptedPass = reader.GetString(index++);
            model.Salt = reader.GetString(index++);
            return model;
        }
    }
}
