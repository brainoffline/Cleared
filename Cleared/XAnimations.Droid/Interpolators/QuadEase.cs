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
using Android.Views.Animations;

namespace XAnimations.Interpolators
{
    public class QuadEaseInInterpolator : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return t * t;
        }
    }

    public class QuadEaseInOutInterpolator : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            t *= 2;
            if (t < 1)
            {
                return 0.5f * t * t;
            }
            --t;
            return -0.5f * (t * (t - 2) - 1);
        }
    }

    public class QuadEaseOutInterpolator : Java.Lang.Object, IInterpolator
    {
        public float GetInterpolation(float t)
        {
            return -t * (t - 2);
        }
    }
}