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

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Developer")]
        public int DeveloperId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Developer")]
        public virtual Developer Developer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        [Required]
        [DisplayName("Icon")]
        public string Icon { get; set; }

        [NotMapped]
        [Required]
        [DisplayName("Upload image")]
        public IFormFile ImageFile { get; set; }
    }
}
