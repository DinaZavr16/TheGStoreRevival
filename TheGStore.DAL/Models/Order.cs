using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGStore.DAL.Models
{
    public class Order : BaseModel
    {
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Game")]
        public int GameId { get; set; }

        [Display(Name = "Date of purchase")]
        public DateTime Date { get; set; }

        [Display(Name = "Customer")]
        public virtual Customer Customer { get; set; }

        [Display(Name = "Game")]
        public virtual Game Game { get; set; }
    }
}
