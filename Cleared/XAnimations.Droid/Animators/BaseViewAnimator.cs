using System;
using Android.Views;
using Android.Animation;
using Android.Views.Animations;
using Android.Util;
using System.Threading.Tasks;
using System.Collections.Generic;
using Java.Lang;

namespace XAnimations
{
    public abstract class BaseViewAnimator : Java.Lang.Object
    {
        public static long DefaultDuration = 1000;

        public AnimatorSet AnimatorAgent { get; } = new AnimatorSet();
        public long Duration { get; set; } = DefaultDuration;
        public ValueAnimatorRepeatMode RepeatMode { get; set; }
        public int RepeatCount { get; set; } = -1;
        public int PauseAfter { get; set; } = 0;
        public bool HasCancelled { get; set; } = false;
        public virtual bool AlphaFromZero => false;
        //protected List<ObjectAnimator> animators;
        View view;

        protected abstract void Prepare(View view);

        public BaseViewAnimator() { }
        public BaseViewAnimator(View view)
        {
            View = view;
        }

        public View View
        {
            set
            {
                view = value;
                if (AlphaFromZero)
                    view.Alpha = 0;
            }
        }

        public void ResetAnimations(bool alphaFromZero = false)
        {
            // only reset properties that are animated
            foreach (ObjectAnimator animator in AnimatorAgent.ChildAnimations)
            {
                if (animator == null)
                    continue;

                if (animator.PropertyName == "alpha")
                {
                    if (alphaFromZero)
                        view.Alpha = 0;
                    else
                        view.Alpha = 1;
                }
                else if (animator.PropertyName == "scaleX") view.ScaleX = 1;
                else if (animator.PropertyName == "scaleY") view.ScaleY = 1;
                else if (animator.PropertyName == "translateX") view.TranslationX = 0;
                else if (animator.PropertyName == "translateY") view.TranslationY = 0;
                else if (animator.PropertyName == "translateZ") view.TranslationZ = 0;
                else if (animator.PropertyName == "rotation") view.Rotation = 0;
                else if (animator.PropertyName == "rotationX") view.RotationX = 0;
                else if (animator.PropertyName == "rotationY") view.RotationY = 0;
                else if (animator.PropertyName == "pivotX") view.PivotX = 0;
                else if (animator.PropertyName == "pivotY") view.PivotY = 0;
            }
        }


        public BaseViewAnimator Start()
        {
            Prepare(view);
            ResetAnimations(AlphaFromZero);

            if (PauseAfter == 0)        // Must manually restart if Pause After
            {
                if (RepeatMode == ValueAnimatorRepeatMode.Restart ||
                    RepeatMode == ValueAnimatorRepeatMode.Reverse)
                {
                    foreach (ObjectAnimator animator in AnimatorAgent.ChildAnimations)
                    {
                        if (animator == null) continue;

                        animator.RepeatMode = RepeatMode;
                        animator.RepeatCount = RepeatCount;
                    }
                }
            }

            AnimatorAgent.SetDuration(Duration);
            AnimatorAgent.Start();
            return this;
        }

        public void Stop()
        {
            HasCancelled = true;
            AnimatorAgent.Cancel();
        }

        public BaseViewAnimator SetDuration(int duration)
        {
            Duration = duration;
            return this;
        }

        public BaseViewAnimator PlayTogether(params ObjectAnimator[] items)
        {
            //animators = new List<ObjectAnimator>(items);
            AnimatorAgent.PlayTogether(items);
            return this;
        }

        public Task Animate()
        {
            var tcs = new TaskCompletionSource<bool>();
            EventHandler animationEnded = null;
            EventHandler animationRepeated = null;
            EventHandler animationCanceled = null;
            bool lastClip = view.ClipToOutline;

            animationEnded = async (s, e) =>
            {
                AnimatorAgent.AnimationEnd -= animationEnded;
                view.SetLayerType(LayerType.None, null);
                tcs.TrySetResult(true);

                if (PauseAfter > 0)        // Must manually restart if Pause After
                {
                    await Task.Delay(PauseAfter);
                    if (!HasCancelled)
                    {
                        StartDelay = 0;
                        Animate();
                    }
                }

            };
            AnimatorAgent.AnimationEnd += animationEnded;

            animationRepeated = (s, e) =>
            {
                AnimatorAgent.AnimationRepeat -= animationRepeated;
                tcs.TrySetResult(false);
            };
            AnimatorAgent.AnimationRepeat += animationRepeated;

            animationCanceled = (s, e) =>
            {
                AnimatorAgent.AnimationCancel -= animationCanceled;
                PauseAfter = 0;
                HasCancelled = true;
                view.SetLayerType(LayerType.None, null);
                tcs.TrySetCanceled();
            };
            AnimatorAgent.AnimationCancel += animationCanceled;

            view.SetLayerType(LayerType.Hardware, null);

            Start();

            return tcs.Task;
        }


        public long StartDelay
        {
            set { AnimatorAgent.StartDelay = value; }
            get { return AnimatorAgent.StartDelay; }
        }

        public BaseViewAnimator SetStartDelay(long delay)
        {
            StartDelay = delay;
            return this;
        }

        public BaseViewAnimator SetPauseAfter(int delay)
        {
            PauseAfter = delay;
            return this;
        }

        /*
        public void Cancel()
        {
            HasCancelled = true;
            AnimatorAgent.Cancel();
        }
        */

        public BaseViewAnimator SetRepeat(ValueAnimatorRepeatMode repeatMode = ValueAnimatorRepeatMode.Restart, int count = Animation.Infinite, int pauseAfter = 0)
        {
            RepeatMode = repeatMode;
            RepeatCount = count;
            PauseAfter = pauseAfter;
            return this;
        }

        public bool IsRunning
        {
            get { return AnimatorAgent.IsRunning; }
        }

        public bool IsStarted
        {
            get { return AnimatorAgent.IsStarted; }
        }

        public BaseViewAnimator AddAnimatorListener(Animator.IAnimatorListener l)
        {
            AnimatorAgent.AddListener(l);
            return this;
        }

        public void RemoveAnimatorListener(Animator.IAnimatorListener l)
        {
            AnimatorAgent.RemoveListener(l);
        }

        public void RemoveAllListeners()
        {
            AnimatorAgent.RemoveAllListeners();
        }

        public IInterpolator Interpolator
        {
            set { AnimatorAgent.SetInterpolator(value); }
        }

        /// <summary>
        /// Android.Resource.Interpolator.LinearOutSlowIn
        /// </summary>
        public BaseViewAnimator SetInterpolator(int interpolatorId)
        {
            AnimatorAgent.SetInterpolator(
                AnimationUtils.LoadInterpolator(view.Context, interpolatorId));
            return this;
        }

        public BaseViewAnimator SetInterpolator(IInterpolator interporlator)
        {
            AnimatorAgent.SetInterpolator(interporlator);
            return this;
        }


        /****/

        public abstract class FloatProperty<T> : FloatProperty 
            where T : Java.Lang.Object 
        {
            public FloatProperty(string name) : base(name)
            { }

            public abstract float GetValue(T t);
            public abstract void SetValue(T t, float value);

            public override Java.Lang.Object Get(Java.Lang.Object obj)
            {
                return GetValue((T)obj);
            }

            public override void SetValue(Java.Lang.Object obj, float value)
            {
                SetValue((T)obj, value);
            }
        }

        public class AlphaProperty : FloatProperty<View>
        {
            public AlphaProperty() : base("alpha") { }

            public override float GetValue(View t) { return t.Alpha; }
            public override void SetValue(View t, float value) { t.Alpha = value; }
        }
        public class TranslationXProperty : FloatProperty<View>
        {
            public TranslationXProperty() : base("translationX") { }

            public override float GetValue(View t) { return t.TranslationX; }
            public override void SetValue(View t, float value) { t.TranslationX = value; }
        }
        public class TranslationYProperty : FloatProperty<View>
        {
            public TranslationYProperty() : base("translationY") { }

            public override float GetValue(View t) { return t.TranslationY; }
            public override void SetValue(View t, float value) { t.TranslationY = value; }
        }
        public class TranslationZProperty : FloatProperty<View>
        {
            public TranslationZProperty() : base("translationZ") { }

            public override float GetValue(View t) { return t.TranslationZ; }
            public override void SetValue(View t, float value) { t.TranslationZ = value; }
        }

        public class XProperty : FloatProperty<View>
        {
            public XProperty() : base("x") { }

            public override float GetValue(View t) { return t.GetX(); }
            public override void SetValue(View t, float value) { t.SetX(value); }
        }
        public class YProperty : FloatProperty<View>
        {
            public YProperty() : base("y") { }

            public override float GetValue(View t) { return t.GetY(); }
            public override void SetValue(View t, float value) { t.SetY(value); }
        }
        public class ZProperty : FloatProperty<View>
        {
            public ZProperty() : base("z") { }

            public override float GetValue(View t) { return t.GetZ(); }
            public override void SetValue(View t, float value) { t.SetZ(value); }
        }

        public class RotationProperty : FloatProperty<View>
        {
            public RotationProperty() : base("rotation") { }

            public override float GetValue(View t) { return t.Rotation; }
            public override void SetValue(View t, float value) { t.Rotation = value; }
        }
        public class RotationXProperty : FloatProperty<View>
        {
            public RotationXProperty() : base("rotationX") { }

            public override float GetValue(View t) { return t.RotationX; }
            public override void SetValue(View t, float value) { t.RotationX = value; }
        }
        public class RotationYProperty : FloatProperty<View>
        {
            public RotationYProperty() : base("rotationY") { }

            public override float GetValue(View t) { return t.RotationY; }
            public override void SetValue(View t, float value) { t.RotationY = value; }
        }

        public class ScaleXProperty : FloatProperty<View>
        {
            public ScaleXProperty() : base("scaleX") { }

            public override float GetValue(View t) { return t.ScaleX; }
            public override void SetValue(View t, float value) { t.ScaleX = value; }
        }
        public class ScaleYProperty : FloatProperty<View>
        {
            public ScaleYProperty() : base("scaleY") { }

            public override float GetValue(View t) { return t.ScaleY; }
            public override void SetValue(View t, float value) { t.ScaleY = value; }
        }

        public class PivotXProperty : FloatProperty<View>
        {
            public PivotXProperty() : base("pivotX") { }

            public override float GetValue(View t) { return t.PivotX; }
            public override void SetValue(View t, float value) { t.PivotX = value; }
        }
        public class PivotYProperty : FloatProperty<View>
        {
            public PivotYProperty() : base("pivotY") { }

            public override float GetValue(View t) { return t.PivotY; }
            public override void SetValue(View t, float value) { t.PivotY = value; }
        }

        public static AlphaProperty ALPHA = new AlphaProperty();
        public static TranslationXProperty TRANSLATION_X = new TranslationXProperty();
        public static TranslationYProperty TRANSLATION_Y = new TranslationYProperty();
        public static TranslationZProperty TRANSLATION_Z = new TranslationZProperty();
        public static XProperty X = new XProperty();
        public static YProperty Y = new YProperty();
        public static ZProperty Z = new ZProperty();
        public static RotationProperty ROTATION = new RotationProperty();
        public static RotationXProperty ROTATION_X = new RotationXProperty();
        public static RotationYProperty ROTATION_Y = new RotationYProperty();
        public static ScaleXProperty SCALE_X = new ScaleXProperty();
        public static ScaleYProperty SCALE_Y = new ScaleYProperty();
        public static PivotXProperty PIVOT_X = new PivotXProperty();
        public static PivotYProperty PIVOT_Y = new PivotYProperty();


    }
}