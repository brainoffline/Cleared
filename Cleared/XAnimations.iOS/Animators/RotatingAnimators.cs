using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using UIKit;

namespace XAnimations
{
    public class RotateInAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(UIView view)
        {
			var animationAlpha = CAKeyFrameAnimation
				.FromKeyPath(Opacity)
				.SetValues(0f, 1f);
			var animationRotate = CAKeyFrameAnimation
                .FromKeyPath(TransformRotation)
				.SetValues(DegreesToRadians(-200), 0f);

			PlayTogether(animationAlpha, animationRotate);
        }
    }

    public class RotateInDownLeftAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(UIView view)
        {
            view.Layer.AnchorPoint = new CoreGraphics.CGPoint(0, 1);

			var animationAlpha = CAKeyFrameAnimation
				.FromKeyPath(Opacity)
				.SetValues(0f, 1f);
			var animationRotate = CAKeyFrameAnimation
				.FromKeyPath(TransformRotation)
				.SetValues(DegreesToRadians(-90), 0f);

			PlayTogether(animationAlpha, animationRotate);
        }
    }

    public class RotateInDownRightAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(UIView view)
        {
			view.Layer.AnchorPoint = new CoreGraphics.CGPoint(1, 1);

			var animationAlpha = CAKeyFrameAnimation
				.FromKeyPath(Opacity)
				.SetValues(0f, 1f);
			var animationRotate = CAKeyFrameAnimation
				.FromKeyPath(TransformRotation)
				.SetValues(DegreesToRadians(90), 0f);

			PlayTogether(animationAlpha, animationRotate);
        }
    }

    public class RotateInUpLeftAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(UIView view)
        {
			view.Layer.AnchorPoint = new CoreGraphics.CGPoint(0, 1);

			var animationAlpha = CAKeyFrameAnimation
				.FromKeyPath(Opacity)
				.SetValues(0f, 1f);
			var animationRotate = CAKeyFrameAnimation
				.FromKeyPath(TransformRotation)
				.SetValues(DegreesToRadians(90), 0f);

			PlayTogether(animationAlpha, animationRotate);
        }
    }

    public class RotateInUpRightAnimator : BaseViewAnimator
    {
        public override bool AlphaFromZero { get { return true; } }

        protected override void Prepare(UIView view)
        {
			view.Layer.AnchorPoint = new CoreGraphics.CGPoint(1, 1);

			var animationRotate = CAKeyFrameAnimation
				.FromKeyPath(TransformRotation)
				.SetValues(DegreesToRadians(-90), 0f);

			PlayTogether(
                CreateKeyFrame(Opacity, 0, 1),
                animationRotate);
        }
    }




    public class RotateOutAnimator : BaseViewAnimator
    {
        protected override void Prepare(UIView view)
        {
            PlayTogether(
                CreateKeyFrame(Opacity, 1, 0),
                CreateKeyFrame(TransformRotation, 0, DegreesToRadians(200))
            );
        }
    }

    public class RotateOutDownLeftAnimator : BaseViewAnimator
    {
        protected override void Prepare(UIView view)
        {
            view.Layer.AnchorPoint = new CoreGraphics.CGPoint(0, 1);
            PlayTogether(
                CreateKeyFrame(Opacity, 1, 0),
                CreateKeyFrame(TransformRotation, 0, DegreesToRadians(90))
            );
        }
    }

    public class RotateOutDownRightAnimator : BaseViewAnimator
    {
        protected override void Prepare(UIView view)
        {
            view.Layer.AnchorPoint = new CoreGraphics.CGPoint(1, 1);
            PlayTogether(
                CreateKeyFrame(Opacity, 1, 0),
                CreateKeyFrame(TransformRotation, 0, DegreesToRadians (-90))
            );
        }
    }

    public class RotateOutUpLeftAnimator : BaseViewAnimator
    {
        protected override void Prepare(UIView view)
        {
            view.Layer.AnchorPoint = new CoreGraphics.CGPoint(0, 1);
            PlayTogether(
                CreateKeyFrame(Opacity, 1, 0),
                CreateKeyFrame(TransformRotation, 0, DegreesToRadians(-90))
            );
        }
    }

    public class RotateOutUpRightAnimator : BaseViewAnimator
    {
        protected override void Prepare(UIView view)
        {
            view.Layer.AnchorPoint = new CoreGraphics.CGPoint(1, 1);
            PlayTogether(
                CreateKeyFrame(Opacity, 1, 0),
                CreateKeyFrame(TransformRotation, 0, DegreesToRadians(90))
            );
        }
    }


}