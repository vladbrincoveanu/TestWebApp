using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Data.Model
{
    public class Game
    {
        public int GameId { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        public virtual Author Author { get; set; }

        public int AuthorID { get; set; }

        public virtual Customer Customer { get; set; }

        public int CustomerID { get; set; }

        public int Price { get; set; }

    }
}
