using System;
using Eto.Forms;
using Eto.Drawing;

namespace Eto.SkiaSharp
{
	[Handler(typeof(IHandler))]
	public class SKView : Panel
	{
		public SKView()
		{
		}
		
		public SKView(IHandler handler)
			: base(handler)
		{
		}
		
		public new interface ICallback : Panel.ICallback
		{
		}
		
		protected new class Callback : Panel.Callback
		{
		}
		
		
		public new interface IHandler : Panel.IHandler
		{
			
		}
	}
}
