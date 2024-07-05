using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Models
{
	public class Pizza
	{
		public int Id { get; set; }
        public string PedidoId { get; set; }
        public List<Sabor> Sabores { get; set; } = new List<Sabor>();
		public double Valor { get; set; }
	}
}
