using System;

namespace XAnimations.Easing
{
    public class SineEaseIn : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return -c * (float)Math.Cos(t / d * (Math.PI / 2f)) + c + b;
        }
    }

    public class SineEaseInOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return -c / 2f * ((float)Math.Cos(Math.PI * t / d) - 1f) + b;
        }
    }

    public class SineEaseOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return c * (float)Math.Sin(t / d * (Math.PI / 2f)) + b;
        }
    }
}