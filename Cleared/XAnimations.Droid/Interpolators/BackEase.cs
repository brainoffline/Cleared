using Android.Views.Animations;

namespace XAnimations.Interpolators
{
    public class BackEaseInInterpolator : Java.Lang.Object, IInterpolator
    {
        private float mOvershot;

        public BackEaseInInterpolator()
        {
            mOvershot = 0;
        }

        public BackEaseInInterpolator(float overshot)
        {
            mOvershot = overshot;
        }

        public float GetInterpolation(float t)
        {
            float s = mOvershot == 0 ? 1.70158f : mOvershot;
            return t * t * ((s + 1f) * t - s);
        }

    }

    public class BackEaseInOutInterpolator : Java.Lang.Object, IInterpolator
    {
        private float mOvershot;

        public BackEaseInOutInterpolator()
        {
            mOvershot = 0;
        }

        public BackEaseInOutInterpolator(float overshot)
        {
            mOvershot = overshot;
        }

        public float GetInterpolation(float t)
        {
            float s = mOvershot == 0 ? 1.70158f : mOvershot;

            t *= 2f;
            if (t < 1)
            {
                s *= (1.525f);
                return 0.5f * (t * t * ((s + 1f) * t - s));
            }

            t -= 2f;
            s *= (1.525f);
            return 0.5f * (t * t * ((s + 1f) * t + s) + 2f);
        }
    }

    public class BackEaseOutInterpolator : Java.Lang.Object, IInterpolator
    {
        private float mOvershot;

        public BackEaseOutInterpolator()
        {
            mOvershot = 0;
        }

        public BackEaseOutInterpolator(float overshot)
        {
            mOvershot = overshot;
        }

        public float GetInterpolation(float t)
        {
            float s = mOvershot == 0 ? 1.70158f : mOvershot;
            t -= 1f;
            return (t * t * ((s + 1f) * t + s) + 1f);
        }
    }
}