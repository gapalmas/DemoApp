using App.Entities;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Web.Data
{
    public class Seeder
    {
        private readonly DataContext context; /* Inyectamos el contexto de la base de datos*/
        private readonly UserManager<User> userManager; /*Usamos el gestor que viene con identity*/
        private readonly Random random; /*Propiedad para generar numeros aleatorios*/


        public Seeder(DataContext context, UserManager<User> userManager) /*Lo inicializamos*/
        {
            this.context = context;
            this.userManager = userManager;
            this.random = new Random();
        }

        public async Task SeederAsync() /*Creamos el metodo*/
        {
            /*Instruccion para esperar a que se cree la base de datos*/
            await context.Database.EnsureCreatedAsync();

            var user = await this.userManager.FindByEmailAsync("gapalmasolano@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Guillermo",
                    LastName = "Palma",
                    Email = "gapalmasolano@gmail.com",
                    UserName = "gapalmasolano@gmail.com",
                    Status = true,
                    RegisterDate = DateTime.Now
                };

                var result = await this.userManager.CreateAsync(user, "102030"); /*Password del UserName en cuestion*/
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("No se puedo crear el usuario con el seeder");
                }
            }

            /*Verificamos si existe algun producto en la base de datos, si no existe crea un tanto*/
            if (!context.Products.Any())
            {
                AddProduct("Xbox 360", user);
                AddProduct("PS3", user);
                AddProduct("Wii", user);
                await context.SaveChangesAsync();
            }


            //TODO: Completar el metodo del Rol para el proximo video
            await this.userManager.AddToRoleAsync(user, "Admin"); /*Agregando el Rol al Usuario, en este caso administrador*/

            var isInRole = await this.userManager.IsInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await this.userManager.AddToRoleAsync(user, "Admin");
            }

            
        }

        private void AddProduct(string name, User user)
        {
            context.Products.Add(new Product
            {
                Name = name,
                Price = random.Next(100),
                Status = true,
                Stock = random.Next(1000),
                User = user
            });
        }
    }
}
