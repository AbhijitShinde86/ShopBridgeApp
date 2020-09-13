using ShopBridge.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class Item
    {
        public int ItemID { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Required(ErrorMessage = "Item Name is required")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [MaxLength(300)]
        [StringLength(300)]
        [Required(ErrorMessage = "Item Description is required")]
        [Display(Name = "Item Description")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Item Price is required")]
        [Display(Name = "Item Price")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }


        public string ImageFileName { get; set; }
    }

    public class ItemResponse : ReturnMessageModel
    {
        public List<Item> Data { get; set; }
    }
}