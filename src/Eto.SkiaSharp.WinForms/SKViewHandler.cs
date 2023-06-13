using System;
using System.Windows;
using Eto;
using Eto.Drawing;
using Eto.WinForms.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

[assembly: Eto.ExportHandler(typeof(Eto.SkiaSharp.SKView), typeof(Eto.SkiaSharp.WinForms.SKViewHandler))]

namespace Eto.SkiaSharp.WinForms;

public class SKViewHandler : WindowsControl<SKControl, SKView, SKView.ICallback>, SKView.IHandler
{
	public override Color BackgroundColor { get; set; }

	protected override SKControl CreateControl() => new SKControl();

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