using System;
using CoreAnimation;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using System.Diagnostics.Contracts;
using Foundation;

namespace XAnimations
{
    public abstract class BaseViewAnimator
    {
        public static double DefaultDuration = 1.0d;

        public const string TranslationX = "transform.translation.x";
        public const string TranslationY = "transform.translation.y";
        public const string TranslationZ = "transform.translation.z";
        //public const string Translation = "transform.translation"; // NSSize or CGSize
        public const string TransformScale = "transform.scale";
        public const string TransformScaleX = "transform.scale.x";
        public const string TransformScaleY = "transform.scale.y";
        public const string TransformScaleZ = "transform.scale.z";
        public const string TransformRotation = "transform.rotation";        // Radians (Z)
        public const string TransformRotateX = "transform.rotation.x";
        public const string TransformRotateY = "transform.rotation.y";
        public const string TransformRotateZ = "transform.rotation.z";
        public const string Opacity = "opacity";
        public const string BoundsSize = "bounds.size";    // CGRect
        public const string BoundsSizeWidth = "bounds.size.width";
        public const string BoundsSizeHeight = "bounds.size.height";
        public const string BoundsOrigin = "bounds.origin";    // CGPoint
        public const string BoundsOriginX = "bounds.origin.x";
        public const string BoundsOriginY = "bounds.origin.y";


        public CAAnimationGroup AnimatorAgent { get; private set; }
        TaskCompletionSource<bool> tcs;

        public double Duration { get; set; } = DefaultDuration;
        public virtual bool AlphaFromZero { get { return false; } }
        UIView view;

        protected abstract void Prepare(UIView view);

        public BaseViewAnimator()
        {
            AnimatorAgent = CAAnimationGroup.CreateAnimation();
            AnimatorAgent.AnimationStarted += (sender, e) => { };
            AnimatorAgent.AnimationStopped += (sender, e) => 
            {
                tcs?.SetResult(true);
            };
        }

        protected float DegreesToRadians(float value)
        {
            return (float)(value * (Math.PI / 180.0f));
        }

        public UIView View
        {
            set
            {
                view = value;
                view?.ResetAnimations(AlphaFromZero);
                //Prepare(view);
            }
        }

        public double StartDelay
        {
            set { AnimatorAgent.BeginTime = CAAnimation.CurrentMediaTime() + value; }
            get { return AnimatorAgent.BeginTime; }
        }

        public float RepeatCount
        {
            set { AnimatorAgent.RepeatCount = value; }
            get { return AnimatorAgent.RepeatCount; }
        }

        public CAMediaTimingFunction TimingFunc
        {
            set { AnimatorAgent.TimingFunction = value; }
            get { return AnimatorAgent.TimingFunction; }
        }

        protected CAKeyFrameAnimation CreateKeyFrame(string keyPath, params float[] values)
        {
            return CAKeyFrameAnimation
                    .FromKeyPath(keyPath)
                    .SetValues(values);
        }

        protected CAKeyFrameAnimation CreateKeyFrame(string keyPath, string timingFuncName, params float[] values)
        {
            return CAKeyFrameAnimation
                    .FromKeyPath(keyPath)
                    .SetTimingFunc((NSString)NSObject.FromObject(timingFuncName))       // CAMediaTimingFunction.Linear
                    .SetValues(values);
        }

        public BaseViewAnimator Start(string name = null)
        {
            Prepare(view);
            AnimatorAgent.Duration = Duration;// + StartDelay;
            AnimatorAgent.FillMode = CAFillMode.Forwards;
            AnimatorAgent.RemovedOnCompletion = false;
            if (AlphaFromZero)
                view.Hidden = false;

            view.Layer.AddAnimation(AnimatorAgent, name);

            return this;
        }

        public bool IsRunning(string name)
        {
            return view.Layer.AnimationForKey(name) != null;
        }

        public bool IsStarted(string name)
        {
            return view.Layer.AnimationForKey(name) != null;
        }


        public BaseViewAnimator PlayTogether(params CAKeyFrameAnimation[] items)
        {
            AnimatorAgent.Animations = items;
            return this;
        }

        public Task Animate(string name = null)
        {
            tcs = new TaskCompletionSource<bool>();

            Start(name);

            return tcs.Task;
        }

        public BaseViewAnimator SetStartDelay(double delay)
        {
            StartDelay = delay;
            return this;
        }

        public BaseViewAnimator SetRepeat(float repeatCount = float.MaxValue)
        {
            AnimatorAgent.RepeatCount = repeatCount;
            return this;
        }

        public BaseViewAnimator SetDuration(double duration)
        {
            Duration = duration;
            return this;
        }

        public BaseViewAnimator SetTimingFunc(CAMediaTimingFunction func)
        {
            AnimatorAgent.TimingFunction = func;
            return this;
        }


        public void Cancel(string name = null)
        {
            if (name == null)
                view.Layer.RemoveAllAnimations();
            else
                view.Layer.RemoveAnimation(name);
        }

        public void Stop()
        {
            Cancel();
        }

    }
}