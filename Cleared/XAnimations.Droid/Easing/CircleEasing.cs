using System;

namespace XAnimations.Easing
{

    public class CircEaseIn : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return -c * ((float)Math.Sqrt(1f - (t /= d) * t) - 1f) + b;
        }
    }

    public class CircEaseInOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            if ((t /= d / 2f) < 1f)
                return -c / 2f * ((float)Math.Sqrt(1f - t * t) - 1f) + b;

            return c / 2f * ((float)Math.Sqrt(1 - (t -= 2f) * t) + 1f) + b;
        }
    }

    public class CircEaseOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return c * (float)Math.Sqrt(1f - (t = t / d - 1f) * t) + b;
        }
    }

}