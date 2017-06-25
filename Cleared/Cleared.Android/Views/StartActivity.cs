using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V4.App;
using Java.Lang;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Cleared.Model;
using XAnimations;
using Cleared.Droid.Engine;
using Cleared.Droid.AndroidUtils;

namespace Cleared.Droid.Views
{
    [Android.App.Activity(
        MainLauncher = true, 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        WindowSoftInputMode = SoftInput.AdjustResize)]
    public class StartActivity : AppCompatActivity
    {
        private SectionsPagerAdapter mSectionsPagerAdapter;
        private ViewPager mViewPager;
        private ImageView logoImage;
        private ImageView nextImage;
        BaseViewAnimator nextAnimator;

        bool created = false;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_start);

            logoImage = FindViewById<ImageView>(Resource.Id.logo);
            logoImage.Clickable = true;
            logoImage.Click += (s, e) =>
            {
                GotoPage(0);
            };

            nextImage = FindViewById<ImageView>(Resource.Id.next);
            nextImage.Clickable = true;
            nextImage.Click += (s, e) =>
            {
                if (mViewPager.CurrentItem < mSectionsPagerAdapter.Count - 1)
                    GotoPage(mViewPager.CurrentItem + 1);
            };

            ResourceUtil.Impl = new Engine.ResourceUtil(this);

            GameManager.Current.LoadKnownGamePacks();
            await GameData.Current.LoadData();

            mSectionsPagerAdapter = new SectionsPagerAdapter( SupportFragmentManager, this, GameManager.Current, GameData.Current);

            mViewPager = FindViewById<ViewPager>(Resource.Id.container);
            mViewPager.Adapter = mSectionsPagerAdapter;
            mViewPager.PageSelected += (s, e) =>
            {
                if (e.Position == 1)
                    StartAnimation();
                else
                    PauseAnimation();
            };
            GotoPage(1, false);        // Home Page

            if (!GameData.Current.MuteSounds)
                MusicManager.Instance.PlayMusic();

            AndroidBug5497Workaround.AssistActivity(this);

            created = true;
        }


        public void GotoPage(int page, bool animate = true)
        {
            mViewPager.SetCurrentItem(page, animate);
        }

        internal void PauseAnimation()
        {
            nextAnimator?.Stop();
            nextAnimator?.ResetAnimations();
            nextAnimator = null;
        }

        internal void StartAnimation()
        {
            nextAnimator = nextImage
                .CreateAnimator<RubberBandAnimator>()
                .SetRepeat(pauseAfter: 2000);
            nextAnimator.Animate();
        }


        public override void OnBackPressed()
        {
            if (mViewPager.CurrentItem != 1)
                GotoPage(1);
            else
                base.OnBackPressed();
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (created)        // Only after async Create tasks
            {
                if (!GameData.Current.MuteSounds)
                    MusicManager.Instance.PlayMusic();
            }

            if (!GameData.Current.FinishedTraining)
            {
                GameManager.Current.CheckTrainingFinished().ConfigureAwait(true);
                if (GameData.Current.FinishedTraining)
                    mSectionsPagerAdapter?.HideTraining();
            }
            else
                mSectionsPagerAdapter?.HideTraining();

            if (GameData.Current.GamesPlayed >= GameData.SHOW_FEEDBACK_AFTER)
                mSectionsPagerAdapter?.ShowFeedback();
        }
    }



    public class SectionsPagerAdapter : FragmentStatePagerAdapter   // FragmentPagerAdapter
    {
        StartActivity startActivity;
        CompanyFragment companyFragment;
        TapToPlayFragment tapToPlayFragment;
        FeedbackFragment feedbackFragment;
        GameManager GameManager;
        GameData GameData;

        bool showTraining;
        bool showFeedback;

        public SectionsPagerAdapter(FragmentManager fm, StartActivity activity, GameManager mgr, GameData gameData) : base(fm)
        {
            startActivity = activity;
            GameManager = mgr;
            GameData = gameData;

            showTraining = !GameData.FinishedTraining;
            showFeedback = GameData.GamesPlayed >= GameData.SHOW_FEEDBACK_AFTER;
        }

        public void HideTraining()
        {
            if (!showTraining)
            {
                showTraining = false;
                NotifyDataSetChanged();
            }
        }

        public void ShowFeedback()
        {
            if (!showFeedback)
            {
                showFeedback = true;
                NotifyDataSetChanged();
            }
        }

        public override int Count
        {
            get
            {
                var pageCount = 2 + GameManager.AllGameSets.Count;
                if (showTraining)
                    pageCount--;
                if (showFeedback)
                    pageCount++;
                return pageCount;
            }
        }

        public override Fragment GetItem(int position)
        {
            if (position == 0)
                return companyFragment ?? (companyFragment = CompanyFragment.CreateInstance());

            if (position == 1)
            {
                if (tapToPlayFragment == null)
                {
                    tapToPlayFragment = new TapToPlayFragment();
                    tapToPlayFragment.Click += (s, e) =>
                    {
                        startActivity.GotoPage(2);
                    };
                }
                return tapToPlayFragment;
            }

            if (position == GameManager.AllGameSets.Count + 1)
                return feedbackFragment ?? (feedbackFragment = FeedbackFragment.CreateInstance());
            
            var gameSetPosition = position - 2;
            if (showTraining)
                gameSetPosition++;  // ignore the first gameset if training complete
            var view = new SelectGameFragment()
            {
                GameSet = GameManager.AllGameSets[gameSetPosition]
            };
            return view;
        }

        public override int GetItemPosition(Java.Lang.Object obj)
        {
            // refresh all fragments when data set changed
            return PagerAdapter.PositionNone;
        }

    }
}