using System;
using System.Windows;
using Eto;
using Eto.Drawing;
using Eto.GtkSharp.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Gtk;

[assembly: Eto.ExportHandler(typeof(Eto.SkiaSharp.SKView), typeof(Eto.SkiaSharp.Gtk.SKViewHandler))]

namespace Eto.SkiaSharp.Gtk;

public class SKViewHandler : GtkControl<SKDrawingArea, SKView, SKView.ICallback>, SKView.IHandler
{
	public override Color BackgroundColor { get; set; }

	protected override SKDrawingArea CreateControl() => new SKDrawingArea();

	public override void AttachEvent(string id)
	{
		switch (id)
		{
			case SKView.PaintSurfaceEvent:
				Control.PaintSurface += Control_PaintSurface;
				break;
			default:
				base.AttachEvent(id);
				break;
		}
	}

	private void Control_PaintSurface(object sender, global::SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
	{
		Callback.OnPaintSurface(Widget, e);
	}

	public SKSize CanvasSize => Control.CanvasSize;

	public bool IgnorePixelScaling { get; set; }
}