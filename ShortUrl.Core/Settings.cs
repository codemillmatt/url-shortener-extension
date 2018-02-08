using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ShortUrl.Core
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string ShortenerUrlKey = "shortener_url_key";
        private static readonly string SettingsDefault = "none yet";

        #endregion


        public static string ShortenerUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(ShortenerUrlKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ShortenerUrlKey, value);
            }
        }

    }
}
