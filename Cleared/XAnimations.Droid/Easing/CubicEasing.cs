using System;

namespace XAnimations.Easing
{
    public class CubicEaseIn : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t + b;
        }
    }
    public class CubicEaseInOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            if ((t /= d / 2f) < 1f)
                return c / 2f * t * t * t + b;

            return c / 2f * ((t -= 2f) * t * t + 2f) + b;
        }
    }
    public class CubicEaseOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1f) * t * t + 1f) + b;
        }
    }
}