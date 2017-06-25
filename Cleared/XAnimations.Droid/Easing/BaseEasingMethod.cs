using System;
using Android.Animation;

namespace XAnimations.Easing
{
    public abstract class BaseEasingMethod : Java.Lang.Object, ITypeEvaluator
    {
        public static float DefaultDuration = 1000f;

        public float Duration { get; set; } = DefaultDuration;

        public Java.Lang.Object Evaluate(float fraction, Java.Lang.Object startValue, Java.Lang.Object endValue)
        {
            float t = Duration * fraction;
            float b = (float)startValue;
            float c = (float)endValue - (float)startValue;
            float d = Duration;
            float result = Calculate(t, b, c, d);

            RaiseEasing(new EasingValues()
            {
                time = t,
                value = result,
                start = b,
                end = c,
                duration = d
            });

            return result;
        }

        public event EventHandler<EasingValues> EasingListeners;
        protected void RaiseEasing(EasingValues values)
        {
            EasingListeners?.Invoke(this, values);
        }

        public struct EasingValues
        {
            public float time;
            public float value;
            public float start;
            public float end;
            public float duration;
        }

        public BaseEasingMethod() { }
        public BaseEasingMethod(float duration)
        {
            Duration = duration;
        }

        public abstract float Calculate(float t, float b, float c, float d);
    }
}