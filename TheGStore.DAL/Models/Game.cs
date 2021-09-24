using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGStore.DAL.Models
{
    public class Game : BaseModel
    {
        public Game()
        {
            Orders = new HashSet<Order>();
        }

        [NotMapped]
        [Required]
        [DisplayName("Загрузити зображення")]
        public IFormFile ImageFile { get; set; }

        [Required]
        public string Icon { get; set; }

        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Вартість")]
        public decimal Price { get; set; }

        [Display(Name = "Розробник")]
        public int DeveloperId { get; set; }

        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Display(Name = "Розробник")]
        public virtual Developer Developer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
