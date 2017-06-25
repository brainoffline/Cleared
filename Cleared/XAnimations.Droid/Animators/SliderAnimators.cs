using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Animation;

namespace XAnimations
{

    public class SlideInDownAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            int distance = view.Top + view.Height;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, -distance, 0)
            );
        }
    }

    public class SlideInLeftAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            ViewGroup parent = (ViewGroup)view.Parent;
            int distance = parent.Width - view.Left;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, -distance, 0)
            );
        }
    }

    public class SlideInRightAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            ViewGroup parent = (ViewGroup)view.Parent;
            int distance = parent.Width - view.Left;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, distance, 0)
            );
        }
    }

    public class SlideInUpAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            ViewGroup parent = (ViewGroup)view.Parent;
            int distance = parent.Height - view.Top;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, distance, 0)
            );
        }
    }


    public class SlideOutDownAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            ViewGroup parent = (ViewGroup)view.Parent;
            int distance = parent.Height - view.Top;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, 0, distance)
            );
        }
    }

    public class SlideOutLeftAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, 0, -view.Right)
            );
        }
    }

    public class SlideOutRightAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            ViewGroup parent = (ViewGroup)view.Parent;
            int distance = parent.Width - view.Left;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, 0, distance)
            );
        }
    }

    public class SlideOutUpAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, 0, -view.Bottom)
            );
        }
    }

}