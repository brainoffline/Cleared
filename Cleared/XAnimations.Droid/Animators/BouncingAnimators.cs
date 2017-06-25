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
using XAnimations.Interpolators;
using Android.Views.Animations;

namespace XAnimations
{
    public class BounceInAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1, 1, 1),
                ObjectAnimator.OfFloat(view, SCALE_X, 0.3f, 1.05f, 0.9f, 1),
                ObjectAnimator.OfFloat(view, SCALE_Y, 0.3f, 1.05f, 0.9f, 1)
                );
        }
    }

    public class BounceInDownAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1, 1, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, -view.Height, 30, -10, 0)
            );
        }
    }

    public class BounceInLeftAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, TRANSLATION_X, view.Width, -30, 10, 0),
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1, 1, 1)
            );
        }
    }

    public class BounceInRightAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, TRANSLATION_X, -view.Width, 30, -10, 0),
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1, 1, 1)
            );
        }
    }

    public class BounceInUpAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, TRANSLATION_Y, view.MeasuredHeight, -30, 10, 0),
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1, 1, 1)
            );
            SetInterpolator(new BackEaseOutInterpolator());
        }
    }




    public class BounceOutAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 1, 0),
                ObjectAnimator.OfFloat(view, SCALE_X, 1f, 0.9f, 1.05f, 0.3f),
                ObjectAnimator.OfFloat(view, SCALE_Y, 1f, 0.9f, 1.05f, 0.3f)
                );
        }
    }

    public class BounceOutDownAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 1, 0),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, 0, 10, -30, view.Height)
            );
        }
    }

    public class BounceOutLeftAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, TRANSLATION_X, 0, -10, 30, -view.Width),
                ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 1, 0)
            );
        }
    }

    public class BounceOutRightAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, TRANSLATION_X, 0, 10,-30, view.Width),
                ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 1, 0)
            );
        }
    }

    public class BounceOutUpAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, TRANSLATION_Y, 0, -10, 30, -view.Height),
                ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 1, 0)
            );
            SetInterpolator(new BackEaseOutInterpolator());
        }
    }

}