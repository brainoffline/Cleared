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
    public class RotateInAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            PlayTogether(
                ObjectAnimator.OfFloat(view, ROTATION, -200, 0),
                ObjectAnimator.OfFloat(view, ALPHA, 0, 1)
                );
        }
    }

    public class RotateInDownLeftAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            float x = view.PaddingLeft;
            float y = view.Height - view.PaddingBottom;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ROTATION, -90, 0),
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, PIVOT_X, x, x),
                    ObjectAnimator.OfFloat(view, PIVOT_Y, y, y)
            );
        }
    }

    public class RotateInDownRightAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            float x = view.Width - view.PaddingRight;
            float y = view.Height - view.PaddingBottom;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ROTATION, 90, 0),
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, PIVOT_X, x, x),
                    ObjectAnimator.OfFloat(view, PIVOT_Y, y, y)
            );
        }
    }

    public class RotateInUpLeftAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            float x = view.PaddingLeft;
            float y = view.Height - view.PaddingBottom;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ROTATION, 90, 0),
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, PIVOT_X, x, x),
                    ObjectAnimator.OfFloat(view, PIVOT_Y, y, y)
            );
        }
    }

    public class RotateInUpRightAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(View view)
        {
            float x = view.Width - view.PaddingRight;
            float y = view.Height - view.PaddingBottom;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ROTATION, -90, 0),
                    ObjectAnimator.OfFloat(view, ALPHA, 0, 1),
                    ObjectAnimator.OfFloat(view, PIVOT_X, x, x),
                    ObjectAnimator.OfFloat(view, PIVOT_Y, y, y)
            );
        }
    }




    public class RotateOutAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, ROTATION, 0, 200)
            );
        }
    }

    public class RotateOutDownLeftAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            float x = view.PaddingLeft;
            float y = view.Height - view.PaddingBottom;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, ROTATION, 0, 90),
                    ObjectAnimator.OfFloat(view, PIVOT_X, x, x),
                    ObjectAnimator.OfFloat(view, PIVOT_Y, y, y)
            );
        }
    }

    public class RotateOutDownRightAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            float x = view.Width - view.PaddingRight;
            float y = view.Height - view.PaddingBottom;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, ROTATION, 0, -90),
                    ObjectAnimator.OfFloat(view, PIVOT_X, x, x),
                    ObjectAnimator.OfFloat(view, PIVOT_Y, y, y)
            );
        }
    }

    public class RotateOutUpLeftAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            float x = view.PaddingLeft;
            float y = view.Height - view.PaddingBottom;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, ROTATION, 0, -90),
                    ObjectAnimator.OfFloat(view, PIVOT_X, x, x),
                    ObjectAnimator.OfFloat(view, PIVOT_Y, y, y)
            );
        }
    }

    public class RotateOutUpRightAnimator : BaseViewAnimator
    {
        protected override void Prepare(View view)
        {
            float x = view.Width - view.PaddingRight;
            float y = view.Height - view.PaddingBottom;
            PlayTogether(
                    ObjectAnimator.OfFloat(view, ALPHA, 1, 0),
                    ObjectAnimator.OfFloat(view, ROTATION, 0, 90),
                    ObjectAnimator.OfFloat(view, PIVOT_X, x, x),
                    ObjectAnimator.OfFloat(view, PIVOT_Y, y, y)
            );
        }
    }


}