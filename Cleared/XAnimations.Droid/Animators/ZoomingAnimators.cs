using System;

using Android.Views;
using Android.Animation;

namespace XAnimations
{
    public class ZoomInAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, SCALE_X, 0.45f, 1),
                ObjectAnimator.OfFloat(view, SCALE_Y, 0.45f, 1),
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1)
                );
        }
    }

    public class ZoomInDownAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, SCALE_X, 0.1f, 0.475f, 1),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 0.1f, 0.475f, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, -view.Bottom, 60, 0),
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1, 1)
            );
        }
    }

    public class ZoomInLeftAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, SCALE_X, 0.1f, 0.475f, 1),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 0.1f, 0.475f, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, -view.Right, 48, 0),
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1, 1)
            );
        }
    }

    public class ZoomInRightAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, SCALE_X, 0.1f, 0.475f, 1),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 0.1f, 0.475f, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, view.Width + view.PaddingRight, -48, 0),
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1, 1)
            );
        }
    }

    public class ZoomInUpAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            ViewGroup parent = (ViewGroup)view.Parent;
            int distance = parent.Height - view.Top;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1, 1),
                    ObjectAnimator.OfFloat(view, SCALE_X, 0.1f, 0.475f, 1),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 0.1f, 0.475f, 1),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, distance, -60, 0)
            );
        }
    }



    public class ZoomOutAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0, 0),
                    ObjectAnimator.OfFloat(view, SCALE_X, 1, 0.3f, 0),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 1, 0.3f, 0)
            );
        }
    }

    public class ZoomOutDownAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            ViewGroup parent = (ViewGroup)view.Parent;
            int distance = parent.Height - view.Top;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 0),
                    ObjectAnimator.OfFloat(view, SCALE_X, 1, 0.475f, 0.1f),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 1, 0.475f, 0.1f),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, 0, -60, distance)
            );
        }
    }

    public class ZoomOutLeftAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 0),
                    ObjectAnimator.OfFloat(view, SCALE_X, 1, 0.475f, 0.1f),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 1, 0.475f, 0.1f),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, 0, 42, -view.Right)
            );
        }
    }

    public class ZoomOutRightAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            ViewGroup parent = (ViewGroup)view.Parent;
            int distance = parent.Width - parent.Left;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 0),
                    ObjectAnimator.OfFloat(view, SCALE_X, 1, 0.475f, 0.1f),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 1, 0.475f, 0.1f),
                    ObjectAnimator.OfFloat(view, TRANSLATION_X, 0, -42, distance)
            );
        }
    }

    public class ZoomOutUpAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 1, 0),
                    ObjectAnimator.OfFloat(view, SCALE_X, 1, 0.475f, 0.1f),
                    ObjectAnimator.OfFloat(view, SCALE_Y, 1, 0.475f, 0.1f),
                    ObjectAnimator.OfFloat(view, TRANSLATION_Y, 0, 60, -view.Bottom)
            );
        }
    }

}