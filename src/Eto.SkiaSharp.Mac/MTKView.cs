#if Mac64
using System;
using MonoMac.AppKit;
using MonoMac.CoreGraphics;
using MonoMac.Foundation;
using MonoMac.ObjCRuntime;

namespace MonoMac.MetalKit
{
	[Register(nameof(MTKView), true)]
	public class MTKView : NSView
	{
		static readonly IntPtr initWithFrameDevice = Selector.GetHandle("initWithFrame:device:");

		public MTKView(CGRect frame, IntPtr device) : base (NSObjectFlag.Empty)
		{
			if (IsDirectBinding) {
				Handle = MonoMac.ObjCRuntime.Messaging.IntPtr_objc_msgSend_CGRect_IntPtr(this.Handle, initWithFrameDevice, frame, device);
			} else {
				Handle = MonoMac.ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_CGRect_IntPtr (this.SuperHandle, initWithFrameDevice, frame, device);
			}
		}
		
		public MTKView(NativeHandle handle) : base(handle)
		{
		}
	}
}
#endif
