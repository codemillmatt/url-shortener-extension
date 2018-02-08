using System;

using UIKit;
//using ShortUrl.Core;
using ShortUrl.Core;
using CoreFoundation;

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

            shortenedUrlLocation.Text = Settings.ShortenerUrl;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void SaveButton_TouchUpInside(UIButton sender)
        {
            Settings.ShortenerUrl = shortenedUrlLocation.Text;

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
