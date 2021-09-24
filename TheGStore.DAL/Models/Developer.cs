using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGStore.DAL.Models
{
    public class Developer : BaseModel
    {
        public Developer()
        {
            Games = new HashSet<Game>();
        }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Country")]
        public virtual Country Country { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
