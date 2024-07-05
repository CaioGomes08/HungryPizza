using HungryPizza.Domain.Models;
using HungryPizza.Domain.Repositories;
using HungryPizza.Infraestructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPizza.Infraestructure.Repositories
{

	public class SaborRepository : ISaborRepository
	{
		private readonly HungryPizzaDbContext _context;

        public SaborRepository(HungryPizzaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Sabor?> GetSaborById(int id)
		{
			return await _context.Sabores.FindAsync(id);
		}
	}
}
