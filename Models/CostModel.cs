using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CostPrice.Models
{
    public class CostModel
    {
        [Display(Name = "Sell Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime purchaseDate { get; set; }
        [Required]
        [Display(Name = "Shares Sold")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int shareNo { get; set; }

        [Required]
        [Display(Name = "Price per Share")]
        [DataType(DataType.Currency)]
        public double price { get; set; }
       

        

    }
}
