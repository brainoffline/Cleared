// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Cleared.iOS
{
    [Register ("PlayViewController")]
    partial class PlayViewController
    {
        [Outlet]
        UIKit.UIImageView ArrowForward { get; set; }


        [Outlet]
        UIKit.UIButton BackButton { get; set; }


        [Outlet]
        UIKit.UIView FinishedPanel { get; set; }


        [Outlet]
        Cleared.iOS.CollectionViewWithTouch Grid { get; set; }


        [Outlet]
        UIKit.UIButton RestartButton { get; set; }


        [Outlet]
        UIKit.UILabel TopLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ArrowForward != null) {
                ArrowForward.Dispose ();
                ArrowForward = null;
            }

            if (BackButton != null) {
                BackButton.Dispose ();
                BackButton = null;
            }

            if (FinishedPanel != null) {
                FinishedPanel.Dispose ();
                FinishedPanel = null;
            }

            if (Grid != null) {
                Grid.Dispose ();
                Grid = null;
            }

            if (RestartButton != null) {
                RestartButton.Dispose ();
                RestartButton = null;
            }

            if (TopLabel != null) {
                TopLabel.Dispose ();
                TopLabel = null;
            }
        }
    }
}