using System.ComponentModel.DataAnnotations;

namespace ShopAPIWebApp.Models
{
    public class Item
    {
        [Key]
        [Display(Name = "№")]
        public byte ItemID { get; set; }
        [Required]
        [Display(Name = "Ім'я")]
        public string ItemName { get; set; }
        [Display(Name = "Ціна")]
        public decimal ItemPrice { get; set; }
        [Display(Name = "Дод. інформація")]
        public string ItemInfo { get; set; }
    }

}
