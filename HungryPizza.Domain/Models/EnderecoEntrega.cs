using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Models
{
	public class EnderecoEntrega
	{
		public string Nome { get; set; }
		public string Endereco { get; set; }
		public string Telefone { get; set; }
        public string PedidoId { get; set; }
    }
}
