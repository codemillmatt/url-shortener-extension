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

        public async override void DidSelectPost()
        {
            string shortenedUrl = "";

            try
            {
                var inputItem = ExtensionContext.InputItems[0];
                var urlItemProvider = ((NSArray)inputItem.UserInfo[NSExtensionItem.AttachmentsKey]).GetItem<NSItemProvider>(0);

                if (urlItemProvider.HasItemConformingTo(MobileCoreServices.UTType.URL))
                {
                    var result = await urlItemProvider.LoadItemAsync(MobileCoreServices.UTType.URL.ToString(), null) as NSUrl;

                    var myUrl = result?.AbsoluteUrl.ToString();

                    var si = new ShorteningInfo
                    {
                        AppendMedium = false,
                        CampaignName = ContentText,
                        Input = myUrl,
                        MediumName = "twitter",
                        TagMediums = false,
                        TagSource = false
                    };

                    var serialized = JsonConvert.SerializeObject(si);
                    StringContent sc = new StringContent(serialized, Encoding.UTF8, "application/json");

                    var totUrl = new Uri("--YOUR INJEST URL HERE--");
                    var client = new HttpClient();

                    var res = await client.PostAsync(totUrl, sc);

                    var shortUrlString = await res.Content.ReadAsStringAsync();

                    var theShortUrl = JsonConvert.DeserializeObject<List<ShortenedUrls>>(shortUrlString);

                    shortenedUrl = theShortUrl.FirstOrDefault().ShortUrl;

                    UIPasteboard clipboard = UIPasteboard.General;
                    clipboard.String = shortenedUrl;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

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

        public override SLComposeSheetConfigurationItem[] GetConfigurationItems()
        {
            // To add configuration options via table cells at the bottom of the sheet, return an array of SLComposeSheetConfigurationItem here.
            return new SLComposeSheetConfigurationItem[0];
        }
    }
}
