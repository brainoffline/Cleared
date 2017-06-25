// This file has been autogenerated from a class added in the UI designer.

using System;
using System.ComponentModel;
using System.Diagnostics;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Cleared.iOS
{
	public partial class CollectionViewWithTouch : UICollectionView
	{
        public event EventHandler<UITouch> TouchBegan;
        public event EventHandler<UITouch> TouchMoved;
        public event EventHandler<UITouch> TouchEnded;
        public event EventHandler<UITouch> TouchCanceled;


		[Export("initWithFrame:collectionViewLayout:"), DesignatedInitializer]
		public CollectionViewWithTouch(CGRect frame, UICollectionViewLayout layout) : base(frame, layout)
        {
			Debug.WriteLine("");
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected CollectionViewWithTouch(NSObjectFlag t) : base(t)
        {
			Debug.WriteLine("");
		}

		[Export("initWithCoder:"), DesignatedInitializer, EditorBrowsable(EditorBrowsableState.Advanced)]
        public CollectionViewWithTouch(NSCoder coder) : base(coder)
        {
			Debug.WriteLine("");
		}

		public CollectionViewWithTouch (IntPtr handle) : base (handle)
		{
            Debug.WriteLine("");
		}

        public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            var touch = touches.AnyObject as UITouch;
            if (touch != null)
                TouchBegan?.Invoke(this, touch);
        }

        public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            var touch = touches.AnyObject as UITouch;
            if (touch != null)
                TouchMoved?.Invoke(this, touch);
        }

        public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            var touch = touches.AnyObject as UITouch;
            if (touch != null)
                TouchEnded?.Invoke(this, touch);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

			var touch = touches.AnyObject as UITouch;
			if (touch != null)
				TouchCanceled?.Invoke(this, touch);

		}
	}
}
