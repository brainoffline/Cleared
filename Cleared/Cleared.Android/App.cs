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
using Cleared.Droid.Engine;
using Cleared.Model;

namespace Cleared.Droid
{
    [Application(
        AllowBackup = true,
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/AppTheme"
        )]
    public class App : Application
    {
        public static App Instance { get; private set; }

        public App(IntPtr handle, JniHandleOwnership transfer)
            : base(handle,transfer)
        {
            Instance = this;

            FontManager.Init(this);
            MusicManager.Init(this);

        }

        public override void OnCreate()
        {
            base.OnCreate();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
        }

        public override void OnTrimMemory([GeneratedEnum] TrimMemory level)
        {
            base.OnTrimMemory(level);

            switch(level)
            {
                case TrimMemory.UiHidden:
                case TrimMemory.Background:
                    MusicManager.Instance.StopMusic();
                    break;
            }
        }
    }
}