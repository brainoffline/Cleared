using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Cleared.Model;

namespace Cleared.iOS
{
    public partial class StartPageViewController : UIPageViewController
    {
        List<UIViewController> Pages { get; set; }
        UIViewController CompanyVC;
        TapToPlayViewController TapToPlayVC;
        FeedbackViewController FeedbackVC;

        public StartPageViewController(IntPtr handle) : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

			ResourceUtil.Impl = new Engine.ResourceUtil();
            GameManager.Current.LoadKnownGamePacks();
            await GameData.Current.LoadData();

            CompanyVC = Storyboard.InstantiateViewController("CompanyVC");
            TapToPlayVC = Storyboard.InstantiateViewController("TapToPlayVC") as TapToPlayViewController;
            FeedbackVC = Storyboard.InstantiateViewController("FeedbackVC") as FeedbackViewController;

            Pages = new List<UIViewController> {  CompanyVC, TapToPlayVC };
            DataSource = new StartPageDataSource(Pages, FeedbackVC, GameManager.Current, Storyboard);

            GotoPage(1);

            TapToPlayVC.Tapped += (sender, e) => 
            {
                GotoPage(2);
            };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

			if (!GameData.Current.FinishedTraining)
			{
				GameManager.Current.CheckTrainingFinished().ConfigureAwait(true);
                if (GameData.Current.FinishedTraining)
                {
                    var trainingPage = PresentedViewController;

					Pages = new List<UIViewController> { CompanyVC, TapToPlayVC };
                    DataSource = new StartPageDataSource(Pages, FeedbackVC, GameManager.Current, Storyboard);

                    // Clear previous page
					GotoPage(0);
					GotoPage(1);
					GotoPage(2);
				}
			}

            /*
			if (GameData.Current.GamesPlayed >= GameData.SHOW_FEEDBACK_AFTER)
				mSectionsPagerAdapter?.ShowFeedback();
			*/
		}


        void GotoPage(int page)
        {
            SetViewControllers(
                new[] { Pages[page] },
                UIPageViewControllerNavigationDirection.Forward,
                true, null);
        }
	}

    public class StartPageDataSource : UIPageViewControllerDataSource
    {
        List<UIViewController> Pages { get; set; }
        int index = 0;
        private GameManager gameManager;
        bool trainingHidden;

        public StartPageDataSource(List<UIViewController> pages, UIViewController lastVC, GameManager current, UIStoryboard storyboard)
        {
            Pages = pages;
            gameManager = current;
            trainingHidden = GameData.Current.FinishedTraining;

            foreach (var gameSet in gameManager.AllGameSets)
            {
                var vc = storyboard.InstantiateViewController("SelectGameVC") as SelectGameViewController;
                vc.GameSet = gameSet;

                if (!trainingHidden || !gameSet.IsTraining)
                    Pages.Add(vc);
            }

            Pages.Add(lastVC);
        }

        public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            index = Pages.IndexOf(referenceViewController);
            if (index > 0)
                return Pages[index - 1];
            return null;
        }

        public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            index = Pages.IndexOf(referenceViewController);
            if (index < Pages.Count - 1)
                return Pages[index + 1];
            return null;
        }
    }
}

