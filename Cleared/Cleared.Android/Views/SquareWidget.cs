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
using Android.Util;
using Cleared.Model;
using Android.Graphics;

namespace Cleared.Droid.Views
{
    public class SquareWidget : FrameLayout
    {
        TextView textView;
        View unfinishedBackground;


        public SquareWidget(Context context) : base(context)
        { Init(); }

        public SquareWidget(Context context, IAttributeSet attrs) : base(context, attrs)
        { Init(); }

        public SquareWidget(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        { Init(); }

        void Init()
        {
            //Inflate(Context, Resource.Layout.widget_square, this);
            //textView = FindViewById<TextView>(Resource.Id.text);
            //unfinishedBackground = FindViewById<View>(Resource.Id.unfinished_background);

            /*
            
            <FrameLayout xmlns:android="http://schemas.android.com/apk/res/android"
                xmlns:tools="http://schemas.android.com/tools"
                android:layout_width="match_parent"
                android:layout_height="match_parent"
                >

                <View
                    android:id="@+id/unfinished_background"
                    android:layout_width="match_parent"
                    android:layout_height="match_parent"
                    android:background="@color/game_unfinished_background" />

                <TextView
                    android:id="@+id/text"
                    android:layout_width="wrap_content"
                    android:layout_height="wrap_content"
                    android:layout_gravity="center"
                    android:layout_margin="@dimen/margin_small"
                    android:fontFamily="sans-serif-light"
                    android:textAlignment="gravity"
                    android:textColor="@color/white"
                    android:textSize="@dimen/square_font_size"
                    tools:text="25" />

            </FrameLayout>
             */

            unfinishedBackground = new View(Context)
            {
                LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
            };
            unfinishedBackground.SetBackgroundColor(DefaultBackgroundColor);
            AddView(unfinishedBackground);

            var px = Resources.GetDimensionPixelSize(Resource.Dimension.margin_small);
            var layout = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            textView = new TextView(Context)
            {
                Gravity = GravityFlags.Center,
                Typeface = DefaultTypeface,
                TextAlignment = TextAlignment.Gravity,
                LayoutParameters = layout
            };
            layout.SetMargins(px, px, px, px);
            textView.SetTextColor(Color.White);
            textView.SetTextSize(ComplexUnitType.Px, Resources.GetDimensionPixelSize(Resource.Dimension.square_font_size));

            AddView(textView);
        }

        static Color defaultBackgroundColor;
        Color DefaultBackgroundColor
        {
            get
            {
                if (defaultBackgroundColor.ToArgb() == 0)
                    defaultBackgroundColor = new Color(Context.GetColor(Resource.Color.game_unfinished_marker));
                return defaultBackgroundColor;
            }
        }

        static Typeface defaultTypeface;
        Typeface DefaultTypeface
        {
            get
            {
                if (defaultTypeface == null)
                    defaultTypeface = Typeface.Create("sans-serif-light", TypefaceStyle.Normal);
                return defaultTypeface;
            }
        }

        public string Text
        {
            get { return textView.Text; }
            set { textView.Text = value; }
        }

        public bool ShowBackground
        {
            get { return unfinishedBackground.Visibility == ViewStates.Visible; }
            set { unfinishedBackground.Visibility = value ? ViewStates.Visible : ViewStates.Invisible; }
        }

        public GameDefinition GameDefinition { get; set; }
    }
}