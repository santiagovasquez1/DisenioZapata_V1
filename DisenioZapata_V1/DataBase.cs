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
    }
}
