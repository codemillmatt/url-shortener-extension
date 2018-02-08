using System;
using System.Net.Http;
using CoreFoundation;
using Foundation;
using Social;
using UIKit;

using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ShortUrl.Core;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharedShortUrl
{
    public partial class ShareViewController : SLComposeServiceViewController
    {
        protected ShareViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override bool IsContentValid()
        {
            return true;
        }

        public override void DidSelectPost()
        {
            string shortenedUrl = "";

            try
            {

                foreach (NSItemProvider itemProvider in this.ExtensionContext.InputItems[0].Attachments)
                {
                    if (itemProvider.HasItemConformingTo(MobileCoreServices.UTType.URL))
                    {
                        itemProvider.LoadItem(MobileCoreServices.UTType.URL, null, async (item, error) =>
                        {
                            if (item is NSUrl)
                            {
                                var urlToShorten = ((NSUrl)item).AbsoluteUrl.ToString();

                                var request = new ShortRequest
                                {
                                    TagWt = false,
                                    TagUtm = false,
                                    Campaign = "test",
                                    Mediums = new List<string>() { "twitter" },
                                    Input = urlToShorten
                                };

                                var serverless = Settings.ShortenerUrl;

                                shortenedUrl = await ShorteningService.ShortenUrl(request);

                                UIPasteboard clipboard = UIPasteboard.General;
                                clipboard.String = shortenedUrl;

                                UIAlertController alert = UIAlertController.Create("Share extension", $"https://{shortenedUrl} has been copied!", UIAlertControllerStyle.Alert);
                                PresentViewController(alert, true, () =>
                                {
                                    var dt = new DispatchTime(DispatchTime.Now, TimeSpan.FromSeconds(3));
                                    DispatchQueue.MainQueue.DispatchAfter(dt, () =>
                                    {
                                        ExtensionContext.CompleteRequest(null, null);
                                    });
                                });
                            }
                        });
                    }
                }

                //ExtensionContext.CompleteRequest(null, null);

                //    var result = await urlItemProvider.LoadItemAsync(MobileCoreServices.UTType.URL.ToString(), null) as NSUrl;

                //    var urlToShorten = result?.AbsoluteUrl.ToString();

                //    var request = new ShortRequest
                //    {
                //        TagWt = false,
                //        TagUtm = false,
                //        Campaign = "test",
                //        Mediums = new List<string>() { "twitter" },
                //        Input = urlToShorten
                //    };

                //    shortenedUrl = await ShorteningService.ShortenUrl(request);

                //    UIPasteboard clipboard = UIPasteboard.General;
                //    clipboard.String = shortenedUrl;
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }

        public override SLComposeSheetConfigurationItem[] GetConfigurationItems()
        {
            // To add configuration options via table cells at the bottom of the sheet, return an array of SLComposeSheetConfigurationItem here.
            return new SLComposeSheetConfigurationItem[0];
        }
    }
}