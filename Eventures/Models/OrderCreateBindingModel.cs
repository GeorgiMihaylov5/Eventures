using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Models
{
    public class OrderCreateBindingModel
    {
        [Required]
        public string EventId { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        [Display(Name = "Tickets")]
        public int TIcketsCount { get; set; }
    }
}
