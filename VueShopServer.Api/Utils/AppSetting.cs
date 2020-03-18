namespace VueShopServer.Api.Utils
{
    public class AppSetting
    {
        // jwt token secret
        public string Secret { get; set; }

        // jwt token expire days
        public int ExpiredDays { get; set; }
    }
}
