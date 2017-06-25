using System;
using Android.Views.Animations;

namespace XAnimations.Interpolators
{

    public class ExpoEaseInInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return (t == 0) ? 0f : (float)Math.Pow(2, 10f * (t - 1f));
        }
    }

    public class ExpoEaseInOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            if (t == 0)
                return 0;

            if (t == 1)
                return 1;

            t *= 2f;
            if (t < 1f)
            {
                return 0.5f * (float)Math.Pow(2, 10f * (t - 1f));
            }

            --t;
            return 0.5f * (float)(-Math.Pow(2, -10f * t) + 2f);
        }
    }

    public class ExpoEaseOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return (t == 1f) ? 1f : (float)(-Math.Pow(2f, -10f * t) + 1f);
        }
    }

}