using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
    public static class SerializacionObjetos
    {
        public static void Serializar(string Ruta,Datos_Zapatas datos)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Ruta, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, datos);
            stream.Close();
        }

        public static Datos_Zapatas Deserealizar(string Ruta)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Stream streamReader = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.None);
            var proyectoDeserializado = (Datos_Zapatas)formatter.Deserialize(streamReader);
            
            streamReader.Close();
            return proyectoDeserializado;
        }
    }
}
