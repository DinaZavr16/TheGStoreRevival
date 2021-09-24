using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGStore.DAL.Models
{
    public class Order : BaseModel
    {
        [Display(Name = "Покупець")]
        public int CustomerId { get; set; }

        [Display(Name = "Гра")]
        public int GameId { get; set; }

        [Display(Name = "Дата покупки")]
        public DateTime Date { get; set; }

        [Display(Name = "Покупець")]
        public virtual Customer Customer { get; set; }

        [Display(Name = "Гра")]
        public virtual Game Game { get; set; }
    }
}
