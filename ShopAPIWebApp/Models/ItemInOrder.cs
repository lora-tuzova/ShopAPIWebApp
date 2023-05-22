namespace ShopAPIWebApp.Models
{
    public class ItemInOrder
    {
        public int Id { get; set; }
        public byte IdItem { get; set; }
        public byte IdOrder { get; set; }
        public int ItemQuantity { get; set; }
        public Item Item { get; set; }
        public Order Order { get; set; }
    }
}
