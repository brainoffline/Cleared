using System;

namespace XAnimations.Easing
{
    public class QuadEaseIn : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return c * (t /= d) * t + b;
        }
    }

    public class QuadEaseInOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            if ((t /= d / 2f) < 1f) return c / 2f * t * t + b;
            return -c / 2f * ((--t) * (t - 2f) - 1f) + b;
        }
    }

    public class QuadEaseOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return -c * (t /= d) * (t - 2f) + b;
        }
    }
}