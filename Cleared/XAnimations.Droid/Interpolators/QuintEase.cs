using Android.Views.Animations;

namespace XAnimations.Interpolators
{
    public class QuintEaseInInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return t * t * t * t * t;
        }
    }

    public class QuintEaseInOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            t *= 2f;
            if (t < 1f)
            {
                return 0.5f * t * t * t * t * t;
            }
            t -= 2f;
            return 0.5f * (t * t * t * t * t + 2f);
        }
    }

    public class QuintEaseOutInterpolater : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            t -= 1f;
            return (t * t * t * t * t + 1f);
        }
    }
}