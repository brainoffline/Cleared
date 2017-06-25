using System;

namespace XAnimations.Easing
{
    public class BackEaseIn : BaseEasingMethod
    {
        private float s = 1.70158f;

        public override float Calculate(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * ((s + 1f) * t - s) + b;
        }
    }

    public class BackEaseInOut : BaseEasingMethod
    {
        private float s = 1.70158f;

        public override float Calculate(float t, float b, float c, float d)
        {
            if ((t /= d / 2f) < 1f)
                return c / 2f * (t * t * (((s *= (1.525f)) + 1f) * t - s)) + b;
            return c / 2f * ((t -= 2f) * t * (((s *= (1.525f)) + 1f) * t + s) + 2f) + b;

        }
    }

    public class BackEaseOut : BaseEasingMethod
    {
        private float s = 1.70158f;

        public override float Calculate(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1f) * t * ((s + 1f) * t + s) + 1f) + b;
        }
    }

}