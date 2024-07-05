
using HungryPizza.Application.ViewModel;
using HungryPizza.Domain.Models;
using HungryPizza.Domain.Repositories;



namespace HungryPizza.Application.Services
{
	public class PedidoService
	{
		private readonly ISaborRepository _saborRepository;
		private readonly IPedidoRepository _pedidoRepository;

		public PedidoService(ISaborRepository saborRepository, IPedidoRepository pedidoRepository)
		{
			_saborRepository = saborRepository;
			_pedidoRepository = pedidoRepository;

		}

		public async Task<string> CriarPedidoAsync(PedidoViewModel model)
		{
			// Criando o pedido
			Pedido pedido = new Pedido();
			pedido.Id = Guid.NewGuid().ToString();

			// Validando a quantidade de pizzas no pedido
			if (!model.Pizzas.Any() || model.Pizzas.Count < 1 || model.Pizzas.Count > 10)
			{
				throw new ArgumentException("Número de pizzas inválido no pedido, o pedido min é de 1 pizza e o máximo de 10 pizzas");
			}

			List<Pizza> pedidoPizzas = new List<Pizza>();
			foreach (var pizza in model.Pizzas)
			{
				var pizzaInsert = new Pizza();
				pizzaInsert.PedidoId = pedido.Id;

				// Validando o sabor
				if (!pizza.Sabores.Any() || pizza.Sabores.Count < 1 || pizza.Sabores.Count > 2)
				{
					throw new ArgumentException("Número de sabores inválido, a pizza deve conter de 1 a 2 sabores apenas");
				}

				foreach (var sabor in pizza.Sabores)
				{
					var saborPizza = await _saborRepository.GetSaborById(sabor.Id);
					if (saborPizza != null)
					{
						pizzaInsert.Sabores.Add(saborPizza);
					}
				}

				pizzaInsert.Valor = pizzaInsert.Sabores.Average(s => s.Valor);
				pedidoPizzas.Add(pizzaInsert);
			}

			// Criando e salvando o endereço
			EnderecoEntrega endereco = new EnderecoEntrega
			{
				PedidoId = pedido.Id,
				Endereco = model.Endereco.Endereco,
				Nome = model.Endereco.Nome,
				Telefone = model.Endereco.Telefone
			};


			// Pedido
			pedido.EnderecoEntrega = endereco;
			pedido.Pizzas = pedidoPizzas;
			pedido.ValorTotal = pedido.Pizzas.Sum(p => p.Valor);
			await _pedidoRepository.Save(pedido);

			return pedido.Id;
		}
	
		public async Task<Pedido> BuscarPedidoPorId(string id)
		{
			var pedido = await _pedidoRepository.BuscarPedidoPorId(id);

			if (pedido == null)
			{
				throw new ArgumentException("Nenhum pedido encontrado");
			}

			return pedido;
		}
	}
}
