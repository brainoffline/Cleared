using System;

namespace XAnimations.Easing
{
    public class LinearEasing : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return c * t / d + b;
        }
    }
}