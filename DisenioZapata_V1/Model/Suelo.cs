using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
    public class Suelo:NotificationObject
    {
		private string tipoSuelo;

		public string TipoSuelo
		{
			get { return tipoSuelo; }
			set { tipoSuelo = value; OnPropertyChanged(); }
		}

		private float sigmaAdmi;

		public float SigmaAdmi
		{
			get { return sigmaAdmi; }
			set { sigmaAdmi = value; OnPropertyChanged(); }
		}
		public Suelo(string nombre,float capacidad)
		{
			TipoSuelo = nombre;
			SigmaAdmi = capacidad;
		}
	}
}
