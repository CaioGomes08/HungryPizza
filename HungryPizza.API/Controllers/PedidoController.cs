
using HungryPizza.Application.Services;
using HungryPizza.Application.ViewModel;
using HungryPizza.Domain.Models;
using HungryPizza.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HungryPizza.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PedidoController : ControllerBase
	{
		private readonly PedidoService _pedidoService;

		public PedidoController(PedidoService pedidoService)
		{
			_pedidoService = pedidoService;

		}

		[HttpPost]
		public async Task<ActionResult<string>> CriarPedido(PedidoViewModel model)
		{
			try
			{
				var pedidoId = await _pedidoService.CriarPedidoAsync(model);
				return Ok(pedidoId);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
			
				return StatusCode(500, "Ocorreu um erro interno no servidor.");
			}
		}

		[HttpGet]
		public async Task<ActionResult<Pedido>> BuscarPedidoPorId(string id)
		{
			try
			{
				return await _pedidoService.BuscarPedidoPorId(id);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{

				return StatusCode(500, "Ocorreu um erro interno no servidor.");
			}

		}
	}
}
