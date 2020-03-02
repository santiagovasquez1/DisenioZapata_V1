﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B_Lectura_E2K.Entidades;

namespace DisenioZapata_V1.Model
{
   public abstract class Zapata:NotificationObject
    {
		private string label;

		public string Label
		{
			get { return label; }
			set { label = value; }
		}

		private float l1;

		public float L1
		{
			get { return l1; }
			set { l1 = value; OnPropertyChanged(); }
		}

		private float l2;

		public float L2
		{
			get { return l2; }
			set { l2 = value; OnPropertyChanged(); }
		}

		private float h;

		public float H
		{
			get { return h; }
			set { h = value; OnPropertyChanged(); }
		}
		private float lcx;

		public float LcX
		{
			get { return lcx; }
			set { lcx = value; OnPropertyChanged(); }
		}
		private float lcy;

		public float LcY
		{
			get { return lcy; }
			set { lcy = value; OnPropertyChanged(); }
		}
		private List<ICalculo> calculos;
		public List<ICalculo> Calculos
		{
			get { return calculos; }
			set { calculos = value; OnPropertyChanged(); }
		}
		
		private MPoint point;
		public MPoint Point
		{
			get { return point; }
			set { point = value; }
		}
		public List<Fuerzas_Modelo> Fuerzas { get; set; }
		public override string ToString()
		{
			return $"ZapataId {Label}";
		}
	}
}
