using System;
using Android.Animation;
using Android.Views.Animations;
using Android.Support.V4.View.Animation;

namespace Cleared.XamNative.Droid
{
    static class InterpolatorUtils
    {
        public const int ACCELERATE_DECELERATE_INTERPOLATOR = 0;
        public const int ACCELERATE_INTERPOLATOR = 1;
        public const int ANTICIPATE_INTERPOLATOR = 2;
        public const int ANTICIPATE_OVERSHOOT_INTERPOLATOR = 3;
        public const int BOUNCE_INTERPOLATOR = 4;
        public const int DECELERATE_INTERPOLATOR = 5;
        public const int FAST_OUT_LINEAR_IN_INTERPOLATOR = 6;
        public const int FAST_OUT_SLOW_IN_INTERPOLATOR = 7;
        public const int LINEAR_INTERPOLATOR = 8;
        public const int LINEAR_OUT_SLOW_IN_INTERPOLATOR = 9;
        public const int OVERSHOOT_INTERPOLATOR = 10;

        public static ITimeInterpolator CreateInterpolator(int interpolatorType)
        {
            switch (interpolatorType)
            {
                case ACCELERATE_DECELERATE_INTERPOLATOR: return new AccelerateDecelerateInterpolator();
                case ACCELERATE_INTERPOLATOR: return new AccelerateInterpolator();
                case ANTICIPATE_INTERPOLATOR: return new AnticipateInterpolator();
                case ANTICIPATE_OVERSHOOT_INTERPOLATOR: return new AnticipateOvershootInterpolator();
                case BOUNCE_INTERPOLATOR: return new BounceInterpolator();
                case DECELERATE_INTERPOLATOR: return new DecelerateInterpolator();
                case FAST_OUT_LINEAR_IN_INTERPOLATOR: return new FastOutLinearInInterpolator();
                case FAST_OUT_SLOW_IN_INTERPOLATOR: return new FastOutSlowInInterpolator();
                case LINEAR_INTERPOLATOR: return new LinearInterpolator();
                case LINEAR_OUT_SLOW_IN_INTERPOLATOR: return new LinearOutSlowInInterpolator();
                case OVERSHOOT_INTERPOLATOR: return new OvershootInterpolator();
                default: return new LinearInterpolator();
            }
        }
    }
}