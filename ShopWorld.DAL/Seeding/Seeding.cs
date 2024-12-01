using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.DAL
{
    public static class Seeding
    {
        public static void InitializeData(this IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Employee.Any())
                {
                    context.Employee.AddRange(
                        new Employee
                        {
                            Name       = "Jane",
                            Surname    = "Doe",
                            IsDeleted  = false
                        },
                        new Employee
                        {
                            Name       = "John",
                            Surname    = "Doe",
                            IsDeleted  = false
                        }
                    );
                }
                if (!context.Item.Any())
                {
                    context.Item.AddRange(
                        new Item 
                        {
                            ImageName   = "117ac3cc-caf5-4ec3-884c-8405c9d28f69.jpg",
                            Description = "Apple",
                            Price       = 100.00m,
                            IsDeleted   = false
                        },
                        new Item
                        {
                            ImageName   = "2701b22f-a83d-4dc3-8c74-c271581acc5e.jpg",
                            Description = "Diamond",
                            Price       = 293.44m,
                            IsDeleted   = false
                        }
                    );
                }
                context.SaveChanges();
            }
        }
    }

}
