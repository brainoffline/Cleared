using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XAnimations.Easing
{
    public class QuartEaseIn : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t + b;
        }
    }

    public class QuartEaseInOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            if ((t /= d / 2f) < 1f)
                return c / 2f * t * t * t * t + b;
            return c / 2f * ((t -= 2f) * t * t * t + 2f) + b;
        }
    }

    public class QuartEaseOut : BaseEasingMethod
    {
        public override float Calculate(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1f) * t * t * t + 1f) + b;
        }
    }
}