﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();

            //context.Database.Migrate();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kajak",
                        Description = "Łódka przeznaczona dla jednej osoby",
                        Category = "Sporty wodne",
                        Price = 275
                    },
                new Product
                {
                    Name = "Kamizelka ratunkowa",
                    Description = "Chroni i dodaje uroku",
                    Category = "Sporty wodne",
                    Price = 48.95M
                },
                new Product
                {
                    Name = "Piłka",
                    Description = "Zatwierdzone przez FIFA rozmiar i waga",
                    Category = "Piłka nożna",
                    Price = 19.50M
                },
                new Product
                {
                    Name = "Flagi narożne",
                    Description = "Nadadzą Twojemu boisku profesjonalny wygląd",
                    Category = "Piłka nożna",
                    Price = 35.95M
                },
                new Product
                {
                    Name = "Stadion",
                    Description = "Składany stadion na 35000 osób",
                    Category = "Piłka nożna",
                    Price = 79500
                },
                new Product
                {
                    Name = "Czapka",
                    Description = "Zwiększa efektywność mózgu o 75%",
                    Category = "Szachy",
                    Price = 16
                },
                new Product
                {
                    Name = "Niestabilne krzesło",
                    Description = "Zmniejsza szanse przeciwnika",
                    Category = "Szachy",
                    Price = 29.95M
                },
                new Product
                {
                    Name = "Ludzka szachownica",
                    Description = "Przyjemna gra dal całej rodziny",
                    Category = "Szachy",
                    Price = 75
                },
                new Product
                {
                    Name = "Błyszczący król",
                    Description = "Figura pokryta złotem i wysadzana diamentami",
                    Category = "Szachy",
                    Price = 1200
                });
                context.SaveChanges();

            }
        }
    }
}
