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
	[Register ("TapToPlayViewController")]
	partial class TapToPlayViewController
	{
		[Outlet]
		UIKit.UIImageView ArrowForward { get; set; }

		[Outlet]
		UIKit.UIImageView BrainImage { get; set; }

		[Outlet]
		UIKit.UIImageView logo { get; set; }

		[Outlet]
		UIKit.UIImageView MusicImage { get; set; }

		[Outlet]
		UIKit.UIButton SoundButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ArrowForward != null) {
				ArrowForward.Dispose ();
				ArrowForward = null;
			}

			if (BrainImage != null) {
				BrainImage.Dispose ();
				BrainImage = null;
			}

			if (MusicImage != null) {
				MusicImage.Dispose ();
				MusicImage = null;
			}

			if (SoundButton != null) {
				SoundButton.Dispose ();
				SoundButton = null;
			}

			if (logo != null) {
				logo.Dispose ();
				logo = null;
			}
		}
	}
}
