using System;

using UIKit;
//using ShortUrl.Core;
using ShortUrl.Core;
using CoreFoundation;
using Foundation;

namespace ShortUrl
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            var defaults = new NSUserDefaults(Constants.GroupName, NSUserDefaultsType.SuiteName);

            var url = defaults.StringForKey(Constants.IOS_SettingsKey);
            if (!string.IsNullOrWhiteSpace(url))
                shortenedUrlLocation.Text = url;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void SaveButton_TouchUpInside(UIButton sender)
        {
            var s = new NSUserDefaults(Constants.GroupName, NSUserDefaultsType.SuiteName);
            s.SetString(shortenedUrlLocation.Text, Constants.IOS_SettingsKey);
            s.Synchronize();

            UIAlertController alert = UIAlertController.Create("Share extension", "Shortening URL has been saved!", UIAlertControllerStyle.Alert);
            PresentViewController(alert, true, () =>
            {
                var dt = new DispatchTime(DispatchTime.Now, TimeSpan.FromSeconds(1));
                DispatchQueue.MainQueue.DispatchAfter(dt, () =>
                {
                    alert.DismissViewController(true, null);
                });
            });
        }
    }
}
