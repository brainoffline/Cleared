using System;

namespace XAnimations.Easing
{
    public abstract class BaseBounceEase : BaseEasingMethod
    {
        protected static float Calc(float t, float b, float c, float d)
        {
            if ((t /= d) < (1f / 2.75f))
            {
                return c * (7.5625f * t * t) + b;
            }
            else if (t < (2f / 2.75f))
            {
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            }
            else if (t < (2.5f / 2.75f))
            {
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            }
            else
            {
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
            }
        }
    }

    public class BounceEaseIn : BaseBounceEase
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return c - Calc(d - t, 0, c, d) + b;
        }
    }

    public class BounceEaseInOut : BaseBounceEase
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            if (t < d / 2f)
            {
                var value = c - Calc(d - (t * 2f), 0, c, d) + b;
                return value * .5f + b;
            }
            else
                return Calc(t * 2f - d, 0, c, d) * .5f + c * .5f + b;
        }
    }

    public class BounceEaseOut : BaseBounceEase
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return Calc(t, b, c, d);
        }
    }

}