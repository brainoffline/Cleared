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
using Android.Graphics;
using Android.Content.Res;

namespace Cleared.Droid.Engine
{
    public static class FontManager
    {
        static AssetManager assetManager;
        static Typeface iconFont;

        public static void Init(Context context)
        {
            assetManager = context.Assets;
        }

        public static Typeface IconFont
        {
            get
            {
                if (iconFont == null)
                    iconFont = Typeface.CreateFromAsset(assetManager, "icons.ttf");
                return iconFont;
            }
        }
    }
}