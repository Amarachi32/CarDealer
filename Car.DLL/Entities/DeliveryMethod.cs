namespace Car.DLL.Entities
{
    public class DeliveryMethod : BaseEntity
    {
        public decimal Price {  get; set; }
        public string Description {  get; set; }
        public string DeliveryTime {  get; set; }
        public string ShortName {  get; set; }
    }
}