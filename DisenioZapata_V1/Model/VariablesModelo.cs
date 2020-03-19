using DisenioZapata_V1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DisenioZapata_V1.Model
{
   public  class VariablesModelo:NotificationObject
    {
		private Suelo suelo;

		public Suelo Suelo
		{
			get { return suelo; }
			set { suelo = value; OnPropertyChanged(); }
		}

		private float deltax;

		public float DeltaX
		{
			get { return deltax; }
			set { deltax = value; OnPropertyChanged(); }
		}

		private float deltay;

		public float DeltaY
		{
			get { return deltay; }
			set { deltay = value; OnPropertyChanged(); }
		}

		private string tiposuelo;

		public string TipoSuelo
		{
			get { return tiposuelo; }
			set { tiposuelo = value;OnPropertyChanged(); }
		}

		private float sigmaadmi;

		public float SigmaAdmi
		{
			get { return sigmaadmi; }
			set { sigmaadmi = value;OnPropertyChanged(); }
		}


		public MiComando AceptarCommand { get; set; }
		public VariablesModelo()
		{
			AceptarCommand = new MiComando(AceptarCommandExecute);
		}

		private void AceptarCommandExecute()
		{
			Suelo = new Suelo(TipoSuelo, SigmaAdmi);
			MessagingCenter.Send(this, "Close");
		}
	}
}
