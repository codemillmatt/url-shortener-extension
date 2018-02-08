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

        public async static Task<string> ShortenUrl(ShortRequest request, string shorteningUrl)
        {
            var shortenedReturn = await HttpService.Post<ShortRequest>(shorteningUrl, request);

            var shortResponseJson = await shortenedReturn.Content.ReadAsStringAsync();

            var theShortUrl = ShortResponse.FromJson(shortResponseJson);

            return theShortUrl.FirstOrDefault()?.ShortUrl;
        }
    }
}
