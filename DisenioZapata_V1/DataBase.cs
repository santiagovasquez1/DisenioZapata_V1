using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            conexion = new MySqlConnection("Server=localhost; Database=Zapatas; Uid=santiagovasquez; Pwd=12345");

            try
            {
                conexion.Open();
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void InsertUser(string UserName,string UserPassword, string UserEmail, string UserIndustry, string UserCountry)
        {
            conexion.Open();
            string Query = $"INSERT INTO users(name,`password`, email, industry, country) " +
                $"Values ('{UserName}',  '{UserPassword}', '{UserEmail}', '{UserIndustry}', '{UserCountry}');";
            
            MySqlCommand command = new MySqlCommand(Query, conexion);
            command.ExecuteNonQuery();
            conexion.Close();
        }

        public bool CheckEmail(string email)
        {
            conexion.Open();
            string Query = $"SELECT email FROM users WHERE email LIKE '{email}'";
            MySqlCommand command = new MySqlCommand(Query, conexion);

            MySqlDataReader registro = command.ExecuteReader();
            if (registro.Read())
            {
                return true;
            }

            return false;
        }

        public bool CheckEmail(string email,string password)
        {
            conexion.Open();
            string Query = $"SELECT email, password FROM users WHERE email LIKE '{email}'";
            MySqlCommand command = new MySqlCommand(Query, conexion);
            MySqlDataReader registro = command.ExecuteReader();

            if (registro.Read())
            {
                if (registro["password"].ToString() == password)
                    return true;
            }

            return false;
        }
    }
}
