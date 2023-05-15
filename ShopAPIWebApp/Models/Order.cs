using System.ComponentModel.DataAnnotations;

namespace ShopAPIWebApp.Models
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<Item> { };
        }
        [Key]
        [Display(Name = "№")]
        public byte OrderID { get; set; }
        [Required]
        [Display(Name = "Замовник")]
        public string OrderOwner { get; set; }
        [Display(Name = "Вартість")]
        public decimal OrderPrice { get; set; }
        [Display(Name = "Дод. інформація")]
        public string OrderInfo { get; set; }
        public virtual ICollection<Item> OrderItems { get; set; }
    }

}
