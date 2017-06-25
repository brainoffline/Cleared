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

    public class FadeInAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1)
                );
        }
    }

    public class FadeInDownAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                ObjectAnimator.OfFloat(view, TRANSLATION_Y, -view.Height / 4, 0)
            );
        }
    }

    public class FadeInLeftAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                ObjectAnimator.OfFloat(view, TRANSLATION_X, -view.Width / 4, 0)
            );
        }
    }

    public class FadeInRightAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                ObjectAnimator.OfFloat(view, TRANSLATION_X, view.Width / 4, 0)
            );
        }
    }

    public class FadeInUpAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                ObjectAnimator.OfFloat(view, TRANSLATION_Y, view.Height / 4, 0)
            );
        }
    }





    public class FadeOutAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, "alpha", 1, 0)
                );
        }
    }

    public class FadeOutDownAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, "alpha", 1, 0),
                ObjectAnimator.OfFloat(view, "translationY", 0, view.Height / 4)
            );
        }
    }

    public class FadeOutLeftAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, "alpha", 1, 0),
                ObjectAnimator.OfFloat(view, "translationX", 0, -view.Width / 4)
            );
        }
    }

    public class FadeOutRightAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, "alpha", 1, 0),
                ObjectAnimator.OfFloat(view, "translationX", 0, view.Width / 4)
            );
        }
    }

    public class FadeOutUpAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, "alpha", 1, 0),
                ObjectAnimator.OfFloat(view, "translationY", 0, -view.Height / 4)
            );
        }
    }

}