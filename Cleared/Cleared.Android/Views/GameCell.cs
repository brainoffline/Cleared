using Android.Content;
using Android.Widget;
using Android.Util;
using Android.Views;
using Cleared.ViewModels;
using System;
using Android.Graphics;
using XAnimations;
using System.Threading.Tasks;
using Cleared.Droid.Engine;
using System.Collections.Generic;
using Android.Graphics.Drawables;

namespace Cleared.Droid.Views
{
    public class GameCell : FrameLayout, IUpdateable
    {
        TextView textView;
        View unfinishedMarker;
        View highlightView;

        BaseViewAnimator animation;

        public GameCell(Context context) : this(context, null)
        { }

        public GameCell(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        { }

        public GameCell(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        { Init(); }



        void Init()
        {
            SetClipChildren(false);

            var px = Resources.GetDimensionPixelSize(Resource.Dimension.piece_margin);
            highlightView = new View(Context)
            {
                LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
            };
            highlightView.SetBackgroundResource(Resource.Drawable.box);

            var markerLayout = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            markerLayout.SetMargins(px, px, px, px);
            unfinishedMarker = new View(Context)
            {
                LayoutParameters = markerLayout
            };
            unfinishedMarker.SetBackgroundResource(Resource.Color.game_unfinished_marker);


            px = Resources.GetDimensionPixelSize(Resource.Dimension.margin_small);
            var textLayout = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            textLayout.SetMargins(px, px, px, px);
            textView = new TextView(Context)
            {
                Gravity = GravityFlags.Center,
                Typeface = DefaultTypeface,
                TextAlignment = TextAlignment.Gravity,
                LayoutParameters = textLayout
            };
            textView.SetTextColor(Color.White);
            textView.SetTextSize(ComplexUnitType.Px, Resources.GetDimensionPixelSize(Resource.Dimension.cell_font_size));
            textView.Visibility = ViewStates.Gone;

            AddView(highlightView);
            AddView(unfinishedMarker);
            AddView(textView);
        }

        public List<Color> ColorPalette { get; set; }

        public string Text
        {
            get { return textView.Text; }
            set
            {
                textView.Text = value;
                textView.Visibility = string.IsNullOrWhiteSpace(value) ? ViewStates.Gone : ViewStates.Visible;
            }
        }


        public bool ShowMarker
        {
            get { return unfinishedMarker.Visibility == ViewStates.Visible; }
            set { unfinishedMarker.Visibility = value ? ViewStates.Visible : ViewStates.Invisible; }
        }

        public Color MarkerColor
        {
            get { return (unfinishedMarker.Background as ColorDrawable).Color; }
            set { unfinishedMarker.Background = new ColorDrawable(value); }
        }


        public bool ShowHighlight
        {
            get { return highlightView.Visibility == ViewStates.Visible; }
            set { highlightView.Visibility = value ? ViewStates.Visible : ViewStates.Invisible; }
        }

        Color highlightColor;
        public Color HighlightColor
        {
            get { return highlightColor; }
            set { highlightView.Background = new ColorDrawable(value); highlightColor = value; }
        }


        public float TextSize
        {
            set { textView.SetTextSize(ComplexUnitType.Px, value); }
        }

        public Typeface Font
        {
            set { textView.SetTypeface(value, TypefaceStyle.Normal); }
        }

        SquareDataViewModel viewModel;
        public SquareDataViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
                DoUpdateView(true);
            }
        }

        async Task DoUpdateView(bool force, int delay = 0)
        {
            if (ViewModel == null)
                return;

            Text = viewModel.Text;

            if (ColorPalette != null && viewModel.Fixed)
            {
                var colorIndex = viewModel.PaletteIndex % ColorPalette.Count;
                MarkerColor = ColorPalette[colorIndex];
            }

            ShowHighlight = viewModel.TouchState != TouchState.UnTouched;

            var newShowMarker = viewModel.MarkerVisible;
            if ((ShowMarker != newShowMarker) || force)
            {
                //animation?.Stop();

                if (!newShowMarker)
                {
                    await Task.WhenAll(new[] {
	                    highlightView
	                        .CreateAnimator<TakingOffAnimator>()
	                        .SetStartDelay(delay)
	                        .SetDuration(300)
	                        .Animate(),
                        unfinishedMarker
							.CreateAnimator<TakingOffAnimator>()
							.SetStartDelay(delay)
							.SetDuration(300)
							.Animate(),
						});
                    ShowMarker = newShowMarker;
                }
                else
                {
                    ShowMarker = newShowMarker;

                    await Task.WhenAll(new[] {
                        highlightView.CreateAnimator<LandingAnimator>().Animate(),
                        unfinishedMarker.CreateAnimator<LandingAnimator>().Animate(),
                    });
                }
            }
        }

        public Task Update(bool force, int delay)
        {
            return DoUpdateView(force, delay);
        }

        static Color defaultMarkerColor;
        Color DefaultMarkerColor
        {
            get
            {
                if (defaultMarkerColor.ToArgb() == 0)
                    defaultMarkerColor = new Color(Context.GetColor(Resource.Color.game_unfinished_marker));
                return defaultMarkerColor;
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

    }
}