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

namespace XAnimations
{
    public static class AnimationExtensions
    {
        public static T CreateAnimator<T>(this View view) where T : BaseViewAnimator, new()
        {
            return new T() { View = view };
        }

        public static BaseViewAnimator CreateAnimator(this View view, Type type)
        {
            var animator = Activator.CreateInstance(type) as BaseViewAnimator;
            animator.View = view;
            return animator;
        }

        public static void ResetAllAnimations(this View view, bool alphaFromZero = false)
        {
            if (alphaFromZero)
                view.Alpha = 0;
            else
                view.Alpha = 1;
            view.ScaleX = 1;
            view.ScaleY = 1;
            view.TranslationX = 0;
            view.TranslationY = 0;
            view.Rotation = 0;
            view.RotationX = 0;
            view.RotationY = 0;
            //view.PivotX = view.Width / 2f;
            //view.PivotY = view.Height / 2f;
        }

    }
}