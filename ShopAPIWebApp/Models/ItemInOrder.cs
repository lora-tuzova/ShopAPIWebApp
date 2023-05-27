namespace ShopAPIWebApp.Models
{
    public class ItemInOrder
    {
        public int Id { get; set; }
        public byte IdItem { get; set; }
        public int IdOrder { get; set; }
        public int ItemQuantity { get; set; }
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
