using System;
using AppKit;
using Eto.Mac.Forms;
using Eto.Mac.Forms.Controls;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Mac;

[assembly: Eto.ExportHandler(typeof(Eto.SkiaSharp.SKView), typeof(Eto.SkiaSharp.Mac.SKViewHandler))]

namespace Eto.SkiaSharp.Mac;

public class EtoSKMetalView : SKMetalView, IMacControl
{
	public WeakReference WeakHandler { get; set; }
}

public class SKViewHandler : MacView<SKMetalView, SKView, SKView.ICallback>, SKView.IHandler
{
	public override NSView ContainerControl => Control;

	public SKSize CanvasSize => Control.CanvasSize;

	public bool IgnorePixelScaling { get; set; }

	protected override SKMetalView CreateControl() => new EtoSKMetalView();
	
	protected override void Initialize()
	{
		base.Initialize();
		// allow transparency
		Control.Layer.Opaque = false;
	}
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

	private void Control_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
	{
		var canvas = e.Surface.Canvas;
		canvas.Clear(SKColors.Transparent);
		
		if (IgnorePixelScaling)
		{
			canvas.Scale((float)Control.Layer.ContentsScale);
			canvas.Save();
		}
		
		Callback.OnPaintSurface(Widget, e);
	}
}
