using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Godspeed.AndroidProject
{
    [Activity(Label = "Godspeed.AndroidProject"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private Game1 game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            game = new Game1(true);            
            SetViewFullScreen();
            game.Run();
        }

        private void SetViewFullScreen()
        {
            var view = (View)game.Services.GetService(typeof(View));
            view.SystemUiVisibility = (StatusBarVisibility)
                (SystemUiFlags.LayoutStable
                | SystemUiFlags.LayoutHideNavigation
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.HideNavigation
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.ImmersiveSticky
                );

            SetContentView(view);
        }
    }
}

