using System;

namespace XAnimations.Easing
{
    public class ExpoEaseIn : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return (t == 0f) ? b : c * (float)Math.Pow(2f, 10f * (t / d - 1f)) + b;
        }
    }

    public class ExpoEaseInOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            if (t == 0f) return b;
            if (t == d) return b + c;
            if ((t /= d / 2) < 1) return c / 2f * (float)Math.Pow(2f, 10f * (t - 1f)) + b;
            return c / 2 * (-(float)Math.Pow(2f, -10f * --t) + 2f) + b;

        }
    }

    public class ExpoEaseOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return (t == d) ? b + c : c * (-(float)Math.Pow(2f, -10f * t / d) + 1f) + b;
        }
    }
}