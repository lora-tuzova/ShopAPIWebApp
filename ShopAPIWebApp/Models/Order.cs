using System.ComponentModel.DataAnnotations;

namespace ShopAPIWebApp.Models
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<ItemInOrder> { };
        }
        [Key]
        [Display(Name = "№")]
        public byte OrderID { get; set; }
        [Required]
        [Display(Name = "Замовник")]
        public string OrderOwner { get; set; }
        [Display(Name = "Вартість")]
        public decimal OrderPrice { get; set; }
        public virtual ICollection<ItemInOrder> OrderItems { get; set; }
    }

}
