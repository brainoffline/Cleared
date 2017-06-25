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
	[Register ("FeedbackViewController")]
	partial class FeedbackViewController
	{
		[Outlet]
		UIKit.UITextView commentEdit { get; set; }

		[Outlet]
		UIKit.UITextField emailText { get; set; }

		[Outlet]
		UIKit.UIButton happy1 { get; set; }

		[Outlet]
		UIKit.UIButton happy2 { get; set; }

		[Outlet]
		UIKit.UIButton happy3 { get; set; }

		[Outlet]
		UIKit.UIButton happy4 { get; set; }

		[Outlet]
		UIKit.UIButton happy5 { get; set; }

		[Outlet]
		UIKit.UIButton sendButton { get; set; }

		[Outlet]
		UIKit.UIStackView stackView { get; set; }

		[Outlet]
		UIKit.UILabel thankYouText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (emailText != null) {
				emailText.Dispose ();
				emailText = null;
			}

			if (happy1 != null) {
				happy1.Dispose ();
				happy1 = null;
			}

			if (happy2 != null) {
				happy2.Dispose ();
				happy2 = null;
			}

			if (happy3 != null) {
				happy3.Dispose ();
				happy3 = null;
			}

			if (happy4 != null) {
				happy4.Dispose ();
				happy4 = null;
			}

			if (happy5 != null) {
				happy5.Dispose ();
				happy5 = null;
			}

			if (sendButton != null) {
				sendButton.Dispose ();
				sendButton = null;
			}

			if (thankYouText != null) {
				thankYouText.Dispose ();
				thankYouText = null;
			}

			if (commentEdit != null) {
				commentEdit.Dispose ();
				commentEdit = null;
			}

			if (stackView != null) {
				stackView.Dispose ();
				stackView = null;
			}
		}
	}
}
