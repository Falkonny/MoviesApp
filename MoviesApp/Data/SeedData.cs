using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Models;

namespace MoviesApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MoviesContext>>()))
            {
                // Look for any movies.
                if (!context.Movies.Any())
                {


                    context.Movies.AddRange(

                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-2-12"),
                            Genre = "Romantic Comedy",
                            Price = 7.99M
                        },


                        new Movie
                        {
                            Title = "Ghostbusters ",
                            ReleaseDate = DateTime.Parse("1984-3-13"),
                            Genre = "Comedy",
                            Price = 8.99M
                        },

                        new Movie
                        {
                            Title = "Ghostbusters 2",
                            ReleaseDate = DateTime.Parse("1986-2-23"),
                            Genre = "Comedy",
                            Price = 9.99M
                        },

                        new Movie
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Genre = "Western",
                            Price = 3.99M
                        }
                    );
                }

                if (!context.Actors.Any())
                {

                    context.Actors.AddRange(
                        new Actor
                        {
                            FirstName = "Nicolas",
                            LastName = "Cage",
                            Birthday = DateTime.Parse("1969-3-19"),
                            Weight = 79,
                            Growth = 181
                        },
                        new Actor
                        {
                            FirstName = "Silvester",
                            LastName = "Stalone",
                            Birthday = DateTime.Parse("1953-5-21"),
                            Weight = 101,
                            Growth = 184
                        },
                        new Actor
                        {
                            FirstName = "Jason",
                            LastName = "Staitham",
                            Birthday = DateTime.Parse("1973-1-10"),
                            Weight = 83,
                            Growth = 179
                        },
                        new Actor
                        {
                            FirstName = "Arnold",
                            LastName = "Shwartzneger",
                            Birthday = DateTime.Parse("1952-7-30"),
                            Weight = 110,
                            Growth = 186
                        }
                    );
                }

                context.SaveChanges();
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();
            }

            if (userManager.FindByEmailAsync("admin@example.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    FirstName = "Super",
                    LastName = "Admin"
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}