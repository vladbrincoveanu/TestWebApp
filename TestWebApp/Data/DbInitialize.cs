using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Model;

namespace TestWebApp.Data
{
    public class DbInitialize
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LibraryDbContext>();


                // Add Customers
                var justin = new Customer { Name = "Justin Noon" , Money = 300};

                var willie = new Customer { Name = "Willie Parodi" , Money = 0};

                var leoma = new Customer { Name = "Leoma  Gosse" , Money = 1000};

                context.Customers.Add(justin);
                context.Customers.Add(willie);
                context.Customers.Add(leoma);

                // Add Author
                Author authorDeMarco = new Author
                {
                    Name = "Activision",
                    Game = new List<Game>()
                {
                    new Game { Name = "Call of duty 2" , Price = 30},
                    new Game { Name = "Destiny" , Price = 20}
                }
                };

                authorDeMarco.Game.ElementAt(0).Author = authorDeMarco;
                authorDeMarco.Game.ElementAt(1).Author = authorDeMarco;

             
                Author authorCardone = new Author
                {
                    Name = "Ubisoft",
                    Game = new List<Game>()
                };

                authorCardone.Game.Add(new Game
                {
                    Name = "Assasin's Creed Origins",
                    Author = authorCardone,
                    Price = 33
                });

                authorCardone.Game.Add(new Game
                {
                    Name = "Tom Clancy's Ranbow",
                    Author = authorCardone,
                    Price = 44
                });

                authorCardone.Game.Add(new Game
                {
                    Name = "Watch Dogs",
                    Price = 22
                });

                context.Authors.Add(authorDeMarco);
                context.Authors.Add(authorCardone);

                context.SaveChanges();
            }
        }
    }
}
