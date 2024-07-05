using HungryPizza.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPizza.Infraestructure.DataContext
{
	public static class InitializeDataBase
	{
		public static void Init(HungryPizzaDbContext context)
		{
			context.Database.EnsureCreated();

			if(context.Sabores.Any())
			{
				// Pois já existe sabores cadastrados
				return;
			}

			var sabores = new Sabor[]
			{
				new Sabor {Nome = "3 Queijos", Valor = 50 },
				new Sabor {Nome = "Frango com Requeijão", Valor = 59.99},
				new Sabor {Nome = "Mussarela", Valor = 42.50 },
				new Sabor {Nome = "Calabresa", Valor = 42.50 },
				new Sabor {Nome = "Pepperoni", Valor = 55 },
				new Sabor {Nome = "Portuguesa", Valor = 45 },
				new Sabor {Nome = "Veggie", Valor = 59.99 },
			};

			foreach (var sabor in sabores)
			{
				context.Sabores.Add(sabor);
			}

			context.SaveChanges();
		}
	}
}
