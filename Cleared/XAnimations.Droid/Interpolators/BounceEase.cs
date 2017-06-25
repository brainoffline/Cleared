using Android.Views.Animations;

namespace XAnimations.Interpolators
{
    public class BounceEaseInInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return 1 - new BounceEaseOutInterpolater().GetInterpolation(1 - t);
        }
    }

    public class BounceEaseInOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            if (t < 0.5f)
            {
                return new BounceEaseInInterpolater().GetInterpolation(t * 2) * 0.5f;
            }
            else
            {
                return new BounceEaseOutInterpolater().GetInterpolation(t * 2 - 1) * 0.5f + 0.5f;
            }
        }
    }

    public class BounceEaseOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            if (t < (1f / 2.75f))
            {
                return 7.5625f * t * t;
            }
            else if (t < (2 / 2.75))
            {
                t -= (1.5f / 2.75f);
                return 7.5625f * t * t + 0.75f;
            }
            else if (t < (2.5f / 2.75f))
            {
                t -= (2.25f / 2.75f);
                return 7.5625f * t * t + 0.9375f;
            }
            else
            {
                t -= (2.625f / 2.75f);
                return 7.5625f * t * t + 0.984375f;
            }
        }
    }
}