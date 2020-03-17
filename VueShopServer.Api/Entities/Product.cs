namespace VueShopServer.Api.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string Detail { get; set; }
    }
}
