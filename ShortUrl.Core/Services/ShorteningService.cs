using System;
using System.Threading.Tasks;
using System.Linq;

namespace ShortUrl.Core
{
    public class ShorteningService
    {
        public ShorteningService()
        {
        }

        public async static Task<string> ShortenUrl(ShortRequest request)
        {
            var shortenedReturn = await HttpService.Post<ShortRequest>(Settings.ShortenerUrl, request);

            var shortResponseJson = await shortenedReturn.Content.ReadAsStringAsync();

            var theShortUrl = ShortResponse.FromJson(shortResponseJson);

            return theShortUrl.FirstOrDefault()?.ShortUrl;
        }
    }
}
