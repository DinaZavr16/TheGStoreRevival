using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGStore.DAL.Models
{
    public class Customer : BaseModel
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
