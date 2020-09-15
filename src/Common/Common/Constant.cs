namespace Common
{
    public static class Constant
    {
        public const int CachingTimeout = 5000; //5s

        public const int MaxImageLength = 5242880;

        public static readonly string[] ListImageType = { "jpg", "JPG", "png", "PNG", "jpeg", "JPEG", "gif", "GIF" };

        public const string AccessTokenHeaderKey = "AccessToken";
        public const string LanguageHeaderKey = "lang";

        public const string AssetsLanguageFolder = "./assets/lang";

        public const double REarth = 6371;
    }
}