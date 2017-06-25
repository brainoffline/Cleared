// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Cleared.iOS
{
	[Register ("GameCell")]
	partial class GameCell
	{
		[Outlet]
		UIKit.UIImageView ArrowView { get; set; }

		[Outlet]
		UIKit.UIView highlightView { get; set; }

		[Outlet]
		UIKit.UIView markerView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ArrowView != null) {
				ArrowView.Dispose ();
				ArrowView = null;
			}

			if (highlightView != null) {
				highlightView.Dispose ();
				highlightView = null;
			}

			if (markerView != null) {
				markerView.Dispose ();
				markerView = null;
			}
		}
	}
}
