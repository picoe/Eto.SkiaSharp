using System;
using System.Windows;
using Eto;
using Eto.Drawing;
using Eto.Wpf.Forms;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using sw = System.Windows;

[assembly: Eto.ExportHandler(typeof(Eto.SkiaSharp.SKView), typeof(Eto.SkiaSharp.Wpf.SKViewHandler))]

namespace Eto.SkiaSharp.Wpf;

public class EtoSKElement : SKElement, IEtoWpfControl
{
	public IWpfFrameworkElement Handler { get; set; }

	protected override sw.Size MeasureOverride(sw.Size availableSize)
	{
		return Handler?.MeasureOverride(availableSize, base.MeasureOverride) ?? base.MeasureOverride(availableSize);
	}
}

public class SKViewHandler : WpfFrameworkElement<SKElement, SKView, SKView.ICallback>, SKView.IHandler
{
	public override Color BackgroundColor { get; set; }

	protected override SKElement CreateControl() => new EtoSKElement { Handler = this };
	
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

	public bool IgnorePixelScaling { get => Control.IgnorePixelScaling; set => Control.IgnorePixelScaling = value; }
}
