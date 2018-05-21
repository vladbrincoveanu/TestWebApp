using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Data.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        public int Money { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public bool LoggedIn { get; set; }
    }
}
