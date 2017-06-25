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
    [Register ("SelectGameViewController")]
    partial class SelectGameViewController
    {
        [Outlet]
        UIKit.UICollectionView Grid { get; set; }


        [Outlet]
        UIKit.UILabel Heading { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Grid != null) {
                Grid.Dispose ();
                Grid = null;
            }

            if (Heading != null) {
                Heading.Dispose ();
                Heading = null;
            }
        }
    }
}