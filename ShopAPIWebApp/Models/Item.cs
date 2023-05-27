using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPIWebApp.Models
{
    public class Item
    {
        public Item() 
            {
                OrderItems = new List<ItemInOrder> { };
            }
        [Key]
        [Display(Name = "№")]
        public byte ItemID { get; set; }
        [Required]
        [Display(Name = "Ім'я")]
        public string ItemName { get; set; }
        [Display(Name = "Ціна")]
        public decimal ItemPrice { get; set; }
        [Range(0, 1)]
        public byte ItemStatus { get; set; } = 1;
        public virtual ICollection<ItemInOrder> OrderItems { get; set; }
    }

}
