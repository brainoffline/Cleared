using System;

using Android.Views;
using Android.Animation;
using XAnimations.Easing;
using XAnimations.Interpolators;

namespace XAnimations
{


    public class DropOutAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            int distance = view.Top + view.Height;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, -distance, 0)
            );
            SetInterpolator(new QuadEaseInInterpolator());
        }
    }

    public class LandingAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, SCALE_X, 1.5f, 1f),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 1.5f, 1f),
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1)
            );
            SetInterpolator(new BackEaseOutInterpolator());
        }
    }

    public class TakingOffAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, SCALE_X, 1f, 1.5f),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 1f, 1.5f),
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0)
            );
            SetInterpolator(new BackEaseOutInterpolator());
        }
    }

    public class HingeAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            float x = view.PaddingLeft;
            float y = view.PaddingTop;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ROTATION, 0, 80, 60, 80, 60, 60),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, 0, 0, 0, 0, 0, 700),
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 1, 1, 1, 0),
                    ObjectAnimator.OfFloat(view, PIVOT_X, x, x, x, x, x, x),
                    ObjectAnimator.OfFloat(view, PIVOT_Y, y, y, y, y, y, y)
            );

            Duration = 1300;
        }
    }

    public class RollInAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, -(view.Width - view.PaddingLeft - view.PaddingRight), 0),
                    ObjectAnimator.OfFloat(view, ROTATION, -120, 0)
            );
        }
    }

    public class RollOutAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, 0, view.Width),
                    ObjectAnimator.OfFloat(view, ROTATION, 0, 120)
            );
        }
    }

}