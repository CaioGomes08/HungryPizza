using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Models
{
	public class Pedido
	{
        public string Id { get; set; }
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
        public EnderecoEntrega EnderecoEntrega { get; set; }
        public double ValorTotal { get; set; }
    }
}
