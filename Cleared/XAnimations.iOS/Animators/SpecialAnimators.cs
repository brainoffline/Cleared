using System;
using CoreAnimation;
using UIKit;

namespace XAnimations
{


    public class DropOutAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(UIView view)
        {
            var distance = view.Frame.Top + view.Frame.Height;
            PlayTogether(
                CreateKeyFrame(Opacity, 0, 1),
                CreateKeyFrame(TranslationX, CAMediaTimingFunction.EaseIn, (float)-distance, 0)
            );
        }
    }

    public class LandingAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(UIView view)
        {
            PlayTogether(
                CreateKeyFrame(TransformScale, CAMediaTimingFunction.EaseOut, 1.5f, 1f),
                CreateKeyFrame(Opacity, 0, 1)
            );
        }
    }

    public class TakingOffAnimator : BaseViewAnimator
    {
        protected override void Prepare(UIView view)
        {
            PlayTogether(
                CreateKeyFrame(TransformScale, CAMediaTimingFunction.EaseOut, 1f, 1.5f),
                CreateKeyFrame(Opacity, 1, 0)
            );
        }
    }
}