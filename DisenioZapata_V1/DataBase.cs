using DisenioZapata_V1.Model.UserModel;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace DisenioZapata_V1
{
    public class DataBase
    {
        private MySqlConnection conexion { get; set; }

        public DataBase()
        {
            Conectar();
        }

        private void Conectar()
        {
            conexion = new MySqlConnection("Server=localhost; Database=zapatas; Uid=clientes; Pwd=cliente123");

            //try
            //{
            //    conexion.Open();
            //    conexion.Close();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}
        }

        public void InsertUser(string UserName, string UserPassword, string UserEmail, string UserIndustry, string UserCountry, out int id)
        {
            conexion.Open();
            string Query = $"INSERT INTO users(name,`password`, email, industry, country) " +
                $"Values ('{UserName}',  '{UserPassword}', '{UserEmail}', '{UserIndustry}', '{UserCountry}');";

            MySqlCommand command = new MySqlCommand(Query, conexion);
            command.ExecuteNonQuery();

            Query = $"SELECT user_id,email, password FROM users WHERE email LIKE '{UserEmail}'";
            command = new MySqlCommand(Query, conexion);

            MySqlDataReader registro = command.ExecuteReader();
            id = 0;

            if (registro.Read())
            {
                id = int.Parse(registro["user_id"].ToString());
            }

            conexion.Close();
        }

        public void InsertOperation(CUser user)
        {
            try
            {
                conexion.Open();
                string Query = $"INSERT INTO operations(`user_id`, ip_dress,finished) " +
                    $"Values ('{user.User_id}',  '{user.IpUser}', '0');";

                MySqlCommand command = new MySqlCommand(Query, conexion);
                command.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool CheckEmail(string email)
        {
            conexion.Open();
            string Query = $"SELECT email FROM users WHERE email LIKE '{email}'";
            MySqlCommand command = new MySqlCommand(Query, conexion);

            MySqlDataReader registro = command.ExecuteReader();
            if (registro.Read())
            {
                conexion.Close();
                return true;
            }

            conexion.Close();
            return false;
        }

        public bool CheckEmail(string email, string password, out int UserId)
        {
            conexion.Open();
            string Query = $"SELECT user_id,email, password FROM users WHERE email LIKE '{email}';";
            MySqlCommand command = new MySqlCommand(Query, conexion);
            MySqlDataReader registro = command.ExecuteReader();

            if (registro.Read())
            {
                if (registro["password"].ToString() == password)
                {
                    UserId = Int32.Parse(registro["user_id"].ToString());
                    conexion.Close();
                    return true;
                }
            }

            UserId = 0;
            conexion.Close();
            return false;
        }
    }
}