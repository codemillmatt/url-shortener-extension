using Foundation;
using System;
using UIKit;

namespace SharedShortUrl
{
    public partial class LinkShortenerViewController : UIViewController, IUITableViewDelegate, IUITableViewDataSource
    {
        public LinkShortenerViewController(IntPtr handle) : base(handle)
        {
            if (optionsTableView != null)
            {
                System.Diagnostics.Debug.WriteLine($"*** YO TABLE VIEW NOT NULL!");
            }
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Section == 0)
            {
                var tc = new UITableViewCell(UITableViewCellStyle.Default, "theID");

                var tv = new UITextView(tc.ContentView.Frame);
                tv.Layer.BorderColor = UIColor.Black.CGColor;
                tv.Layer.BorderWidth = 4;
                tv.AdjustsFontForContentSizeCategory = true;
                tv.Font = UIFont.SystemFontOfSize(24);


                tc.ContentView.AddSubview(tv);

                return tc;
            }

            if (indexPath.Section == 1)
            {
                var tc = new UITableViewCell(UITableViewCellStyle.Default, "newID");
                tc.TextLabel.Text = "TAGS!";

                return tc;
            }

            if (indexPath.Section == 2)
            {
                var tc = new UITableViewCell(UITableViewCellStyle.Default, "newestID");
                tc.TextLabel.Text = "TWEET!";

                return tc;
            }

            return null;
        }

        [Export("tableView:titleForHeaderInSection:")]
        public string TitleForHeader(UITableView tableView, nint section)
        {
            switch (section)
            {
                case 0:
                    return "Campaign";
                case 1:
                    return "Tags";
                case 2:
                    return "Tweet";
                default:
                    return "";
            }
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            switch (section)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 1;
                default:
                    return 1;
            }
        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            return 3;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            optionsTableView.Delegate = this;
            optionsTableView.DataSource = this;
        }
    }
}