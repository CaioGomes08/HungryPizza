using HungryPizza.Domain.Models;
using HungryPizza.Domain.Repositories;
using HungryPizza.Infraestructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPizza.Infraestructure.Repositories
{
	public class PedidoRepository : IPedidoRepository
	{
		private readonly HungryPizzaDbContext _context;

        public PedidoRepository(HungryPizzaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

		public async Task<Pedido?> BuscarPedidoPorId(string id)
		{
		 return await	_context.Pedidos.Include(p => p.Pizzas)
					.ThenInclude(p => p.Sabores)
				.Include(p => p.EnderecoEntrega)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task Save(Pedido pedido)
		{
			_context.Pedidos.Add(pedido);
			await _context.SaveChangesAsync();
		}


	}
}
