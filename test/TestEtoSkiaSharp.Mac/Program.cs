﻿using System;
using Eto.Forms;

namespace TestEtoSkiaSharp.Mac
{
	class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			new Application().Run(new MainForm());
		}
	}
}
