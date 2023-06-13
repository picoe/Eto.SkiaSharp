using System;
using Eto.Forms;
using Eto.Drawing;
using SkiaSharp;

namespace TestEtoSkiaSharp
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			Title = "My Skia Form";

			var skiaView = new Eto.SkiaSharp.SKView
			{
				Size = new Size(200, 200),
				IgnorePixelScaling = true
			};
			skiaView.PaintSurface += (sender, e) =>
			{
				var canvas = e.Surface.Canvas;
				// canvas.Clear(SKColors.Transparent);
				var paint = new SKPaint
				{
					Color = SKColors.Blue,
					StrokeWidth = 1,
					IsAntialias = true,
					Style = SKPaintStyle.Fill,
					TextAlign = SKTextAlign.Center,
					TextSize = 24
				};
				canvas.DrawLine(0, 0, 200, 200, paint);
			};
			Content = skiaView;

			var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
			quitCommand.Executed += (sender, e) => Application.Instance.Quit();

			var aboutCommand = new Command { MenuText = "About..." };
			aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);

			// create menu
			Menu = new MenuBar
			{
				Items =
				{
					// File submenu
					// new SubMenuItem { Text = "&File", Items = { clickMe } },
					// new SubMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new SubMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
				ApplicationItems =
				{
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences..." },
				},
				QuitItem = quitCommand,
				AboutItem = aboutCommand
			};

		}
	}
}
