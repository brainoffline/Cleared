using System;

using Android.OS;
using Android.Views;
using Android.Widget;
using Cleared.Model;
using XAnimations;
using Android.Views.Animations;
using Android.Animation;
using Cleared.Droid.Engine;

namespace Cleared.Droid.Views
{
    public class TapToPlayFragment : Android.Support.V4.App.Fragment
    {
        ViewGroup root;
        ImageButton soundButton;

        public event EventHandler Click;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_taptoplay, container, false);
            root = view.FindViewById<ViewGroup>(Resource.Id.root);

            root.Click += (s, a) =>
            {
                Click?.Invoke(this, new EventArgs());
            };

            soundButton = view.FindViewById<ImageButton>(Resource.Id.soundButton);
            soundButton.Click += async (s, e) =>
            {
                GameData.Current.MuteSounds = !GameData.Current.MuteSounds;
                await GameData.Current.SaveData();
                UpdateScreen(true);
            };
            UpdateScreen(false);

            return view;
        }

        void UpdateScreen(bool changeMusic)
        {
            if (GameData.Current.MuteSounds)
            {
                soundButton.Alpha = 0.2f;
                if (changeMusic)
                    MusicManager.Instance.StopMusic();
            }
            else
            {
                soundButton.Alpha = 0.6f;
                if (changeMusic)
                    MusicManager.Instance.PlayMusic();
            }
        }


    }
}