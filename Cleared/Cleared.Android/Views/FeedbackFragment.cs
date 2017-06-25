using Android.OS;
using Android.Views;
using Android.Widget;
using Cleared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XAnimations;

namespace Cleared.Droid.Views
{
    public class FeedbackFragment : Android.Support.V4.App.Fragment
    {
        ImageButton happy1, happy2, happy3, happy4, happy5;
        EditText commentEdit, emailEdit;
        Button sendButton, rateButton;
        TextView thankYouText;

        FeedbackData Data = new FeedbackData();

        public static FeedbackFragment CreateInstance()
        {
            return new FeedbackFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_feedback, container, false);

            happy1 = view.FindViewById<ImageButton>(Resource.Id.happy1);
            happy2 = view.FindViewById<ImageButton>(Resource.Id.happy2);
            happy3 = view.FindViewById<ImageButton>(Resource.Id.happy3);
            happy4 = view.FindViewById<ImageButton>(Resource.Id.happy4);
            happy5 = view.FindViewById<ImageButton>(Resource.Id.happy5);

            happy1.Click += (s, e) => { Data.Happy = 1; UpdateScreen(); ((View)s).CreateAnimator<TadaAnimator>().Animate(); };
            happy2.Click += (s, e) => { Data.Happy = 2; UpdateScreen(); ((View)s).CreateAnimator<WobbleAnimator>().Animate(); };
            happy3.Click += (s, e) => { Data.Happy = 3; UpdateScreen(); ((View)s).CreateAnimator<WaveAnimator>().Animate(); };
            happy4.Click += (s, e) => { Data.Happy = 4; UpdateScreen(); ((View)s).CreateAnimator<SwingAnimator>().Animate(); };
            happy5.Click += (s, e) => { Data.Happy = 5; UpdateScreen(); ((View)s).CreateAnimator<RubberBandAnimator>().Animate(); };

            commentEdit = view.FindViewById<EditText>(Resource.Id.comment);
            emailEdit = view.FindViewById<EditText>(Resource.Id.emailaddress);
            thankYouText = view.FindViewById<TextView>(Resource.Id.thankyou);

            sendButton = view.FindViewById<Button>(Resource.Id.send_button);
            sendButton.Click += SendButton_Click;
            rateButton = view.FindViewById<Button>(Resource.Id.rate_button);

            UpdateScreen();

            return view;
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            sendButton.Enabled = false;
            commentEdit.Enabled = false;
            emailEdit.Enabled = false;
            happy1.Enabled = false;
            happy2.Enabled = false;
            happy3.Enabled = false;
            happy4.Enabled = false;
            happy5.Enabled = false;

            thankYouText.Visibility = ViewStates.Visible;
            await thankYouText.CreateAnimator<FadeInUpAnimator>()
                .SetDuration(300)
                .Animate();

            Data.Comment = commentEdit.Text;
            Data.Email = emailEdit.Text;
            await BrainCloudService.Current.AddFeedback(Data);


            thankYouText.CreateAnimator<TadaAnimator>()
                .SetRepeat(Android.Animation.ValueAnimatorRepeatMode.Restart)
                .Start();
        }

        void UpdateScreen()
        {
            happy1.Alpha = (Data.Happy == 1) ? 1f : 0.5f;
            happy2.Alpha = (Data.Happy == 2) ? 1f : 0.5f;
            happy3.Alpha = (Data.Happy == 3) ? 1f : 0.5f;
            happy4.Alpha = (Data.Happy == 4) ? 1f : 0.5f;
            happy5.Alpha = (Data.Happy == 5) ? 1f : 0.5f;
        }

    }
}