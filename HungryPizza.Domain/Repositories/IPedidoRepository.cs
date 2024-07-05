using HungryPizza.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Repositories
{
	public interface IPedidoRepository
	{
		public Task Save(Pedido pedido);
		public Task<Pedido?> BuscarPedidoPorId(string id);
	}
}
