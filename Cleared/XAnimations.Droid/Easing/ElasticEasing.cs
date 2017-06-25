using System;

namespace XAnimations.Easing
{
    public class ElasticEaseIn : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            if (t == 0f)
                return b;
            if ((t /= d) == 1f)
                return b + c;
            float p = d * .3f;
            float a = c;
            float s = p / 4f;
            return -(a * (float)Math.Pow(2f, 10f * (t -= 1f)) * (float)Math.Sin((t * d - s) * (2f * (float)Math.PI) / p)) + b;
        }
    }

    public class ElasticEaseInOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            if (t == 0)
                return b;
            if ((t /= d / 2f) == 2f)
                return b + c;
            float p = d * (.3f * 1.5f);
            float a = c;
            float s = p / 4f;
            if (t < 1)
                return -.5f * (a * (float)Math.Pow(2f, 10f * (t -= 1f)) * (float)Math.Sin((t * d - s) * (2f * (float)Math.PI) / p)) + b;
            return a * (float)Math.Pow(2f, -10f * (t -= 1f)) * (float)Math.Sin((t * d - s) * (2f * (float)Math.PI) / p) * .5f + c + b;
        }
    }

    public class ElasticEaseOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            if (t == 0)
                return b;
            if ((t /= d) == 1f)
                return b + c;
            float p = d * .3f;
            float a = c;
            float s = p / 4f;
            return (a * (float)Math.Pow(2f, -10f * t) * (float)Math.Sin((t * d - s) * (2f * (float)Math.PI) / p) + c + b);
        }
    }

}