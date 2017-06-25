using System;
using Android.Views.Animations;

namespace XAnimations.Interpolators
{
    public class ElasticEaseInInterpolater : Java.Lang.Object, IInterpolator
    {
        private float mAmplitude;
        private float mPeriod;

        public ElasticEaseInInterpolater()
        {
            mAmplitude = 0;
            mPeriod = 0;
        }

        public ElasticEaseInInterpolater(float amplitude, float period)
        {
            mAmplitude = amplitude;
            mPeriod = period;
        }

        public float GetInterpolation(float t)
        {
            float p = mPeriod;
            float a = mAmplitude;

            float s;
            if (t == 0)
                return 0;

            if (t == 1)
                return 1;

            if (p == 0)
                p = 0.3f;

            if (a == 0 || a < 1)
            {
                a = 1;
                s = p / 4;
            }
            else
            {
                s = (float)(p / (Math.PI * 2) * Math.Asin(1 / a));
            }
            t -= 1;
            return -(float)(a * Math.Pow(2, 10 * t) * Math.Sin((t - s) * (Math.PI * 2) / p));
        }
    }

    public class ElasticEaseInOutInterpolater : Java.Lang.Object, IInterpolator
    {
        private float mAmplitude;
        private float mPeriod;

        public ElasticEaseInOutInterpolater()
        {
            mAmplitude = 0;
            mPeriod = 0;
        }

        public ElasticEaseInOutInterpolater(float amplitude, float period)
        {
            mAmplitude = amplitude;
            mPeriod = period;
        }

        public float GetInterpolation(float t)
        {
            float p = mPeriod;
            float a = mAmplitude;

            float s;
            if (t == 0)
                return 0;

            t /= 0.5f;
            if (t == 2)
                return 1;

            if (p == 0)
                p = 0.3f * 1.5f;
            if (a == 0 || a < 1)
            {
                a = 1;
                s = p / 4;
            }
            else
            {
                s = (float)(p / (Math.PI * 2) * Math.Asin(1 / a));
            }
            if (t < 1)
            {
                t -= 1;
                return -0.5f * (float)(a * Math.Pow(2, 10 * t) * Math.Sin((t - s) * (Math.PI *
                        2) / p));
            }

            t -= 1;
            return (float)(a * Math.Pow(2, -10 * t) * Math.Sin((t - s) * (Math.PI * 2) / p) *
                    0.5f + 1);
        }
    }

    public class ElasticEaseOutInterpolater : Java.Lang.Object, IInterpolator
    {
        private float mAmplitude;
        private float mPeriod;

        public ElasticEaseOutInterpolater()
        {
            mAmplitude = 0;
            mPeriod = 0;
        }

        public ElasticEaseOutInterpolater(float amplitude, float period)
        {
            mAmplitude = amplitude;
            mPeriod = period;
        }

        public float GetInterpolation(float t)
        {
            float p = mPeriod;
            float a = mAmplitude;

            float s;
            if (t == 0)
                return 0;

            if (t == 1)
                return 1;

            if (p == 0)
                p = 0.3f;

            if (a == 0 || a < 1)
            {
                a = 1;
                s = p / 4;
            }
            else
            {
                s = (float)(p / (Math.PI * 2) * Math.Asin(1 / a));
            }
            return (float)(a * Math.Pow(2, -10 * t) * Math.Sin((t - s) * (Math.PI * 2) / p) + 1);
        }
    }
}