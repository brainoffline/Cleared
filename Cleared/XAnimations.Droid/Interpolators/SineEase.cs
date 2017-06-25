using System;
using Android.Views.Animations;

namespace XAnimations.Interpolators
{
    public class SineEaseInInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return -(float)Math.Cos(t * (Math.PI / 2)) + 1;
        }
    }

    public class SineEaseInOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return -0.5f * (float)(Math.Cos(Math.PI * t) - 1);
        }
    }

    public class SineEaseOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return (float)Math.Sin(t * (Math.PI / 2));
        }
    }
}
