using HungryPizza.Application.Services;
using HungryPizza.Application.ViewModel;
using HungryPizza.Domain.Models;
using HungryPizza.Domain.Repositories;
using Moq;
using Xunit;

namespace HungryPizza.Test.Services
{
	public class PedidoServiceTests
	{
		private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
		private readonly Mock<ISaborRepository> _saborRepositoryMock;
		private readonly PedidoService _pedidoService;

        public PedidoServiceTests()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
			_saborRepositoryMock = new Mock<ISaborRepository>();
			_pedidoService = new PedidoService(_saborRepositoryMock.Object, _pedidoRepositoryMock.Object);
        }

		[Fact]
		public async Task CriarPedido_DeveRetornarIdPedido_QuandoCriadoComSucesso()
		{
			// Arrange
			var pedidoViewModel = new PedidoViewModel
			{
				Pizzas = new List<PizzaViewModel>
				{
					new PizzaViewModel
					{
						Sabores = new List<SaborViewModel>
						{
							new SaborViewModel { Id = 1 },
							new SaborViewModel { Id = 2 }
						}
					}
				},
				Endereco = new EnderecoEntregaViewModel
				{
					Endereco = "Rua de Teste, 333",
					Nome = "Caio Gomes",
					Telefone = "992234433"
				}
			};

			// Criando os sabores
			List < Sabor > sabores = new List<Sabor>
			{
				 new Sabor { Id = 1, Nome = "Calabresa", Valor = 10.0 },
				 new Sabor { Id = 2, Nome = "Mussarela", Valor = 12.0 }
			};
			

			_saborRepositoryMock.Setup(sr => sr.GetSaborById(1)).ReturnsAsync(sabores.Where(s => s.Id == 1).First());
			_saborRepositoryMock.Setup(sr => sr.GetSaborById(2)).ReturnsAsync(sabores.Where(s => s.Id == 2).First());

			_pedidoRepositoryMock.Setup(repo => repo.Save(It.IsAny<Pedido>())).Returns(Task.CompletedTask);

			// Act
			var pedidoId = await _pedidoService.CriarPedidoAsync(pedidoViewModel);

			// Assert
			Xunit.Assert.NotNull(pedidoId);
			_pedidoRepositoryMock.Verify(repo => repo.Save(It.IsAny<Pedido>()), Times.Once);
		}
    }
}
