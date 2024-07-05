using HungryPizza.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPizza.Infraestructure.DataContext
{
	public class HungryPizzaDbContext : DbContext
	{
		public HungryPizzaDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<EnderecoEntrega> EnderecoEntrega { get; set; }
        public DbSet<Sabor> Sabores { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Pedido>()
				.HasMany(p => p.Pizzas)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Pedido>()
				.OwnsOne(p => p.EnderecoEntrega);

			modelBuilder.Entity<Pizza>()
				.HasMany(p => p.Sabores)
				.WithMany()
				.UsingEntity(j => j.ToTable("PizzaSabores"));
		}
	}
}
