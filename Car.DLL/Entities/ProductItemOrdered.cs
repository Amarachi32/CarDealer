namespace Car.DLL.Entities
{
    public class ProductItemOrdered : BaseEntity
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(string id, string productName, string productUrl)
        {
            Id = id;
            ProductName = productName;
            ProductUrl = productUrl;
        }

        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
    }
}