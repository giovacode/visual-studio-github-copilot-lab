namespace Store.Helpers
{
    public static class ImageHelper
    {
        public static string GetFullImageUrl(string? imagePrefix, string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return string.Empty;
            if (string.IsNullOrEmpty(imagePrefix))
                return imageUrl;
            return $"{imagePrefix.TrimEnd('/')}/{imageUrl.TrimStart('/')}";
        }
    }
}