using System;
using Eto.Forms;
using Eto.Drawing;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace Eto.SkiaSharp
{
	[Handler(typeof(IHandler))]
	public class SKView : Control
	{
		new IHandler Handler => (IHandler)base.Handler;
		
		public const string PaintSurfaceEvent = "PaintSurface";
		
		public event EventHandler<SKPaintSurfaceEventArgs> PaintSurface
		{
			add => Properties.AddHandlerEvent(PaintSurfaceEvent, value);
			remove => Properties.RemoveEvent(PaintSurfaceEvent, value);
		}
		
		protected virtual void OnPaintSurface(SKPaintSurfaceEventArgs e) => Properties.TriggerEvent(PaintSurfaceEvent, this, e);

		public SKSize CanvasSize => Handler.CanvasSize;
		
        public bool IgnorePixelScaling
		{
			get => Handler.IgnorePixelScaling;
			set => Handler.IgnorePixelScaling = value;
		}
		
		public SKView()
		{
		}
		
		public SKView(IHandler handler)
			: base(handler)
		{
		}
		
		public new interface ICallback : Control.ICallback
		{
			void OnPaintSurface(SKView widget, SKPaintSurfaceEventArgs e);
		}

		static readonly Callback _callback = new Callback();

		protected override object GetCallback() => _callback;

		protected new class Callback : Control.Callback, ICallback
		{
			public void OnPaintSurface(SKView widget, SKPaintSurfaceEventArgs e)
			{
				using (widget.Platform.Context)
					widget.OnPaintSurface(e);
			}
		}

		public new interface IHandler : Control.IHandler
		{
			SKSize CanvasSize { get; }
			bool IgnorePixelScaling { get; set; }
		}
	}
}
