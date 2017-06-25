using System;
using Android.Views.Animations;

namespace XAnimations.Interpolators
{
    public class CircleEaseInInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return -(float)(Math.Sqrt(1f - t * t) - 1f);
        }
    }

    public class CircleEaseOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return (float)Math.Sqrt(1f - (t -= 1f) * t);
        }
    }

    public class CircleEaseInOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            t *= 2f;
            if (t < 1f)
            {
                return -0.5f * (float)(Math.Sqrt(1f - t * t) - 1f);
            }

            t -= 2f;
            return 0.5f * (float)(Math.Sqrt(1f - t * t) + 1f);
        }
    }
}