using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MaestroConceptoRepository : IMaestroConceptoRepository
    {
        private readonly SqlDbContext _context;

        public MaestroConceptoRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaestroConceptoNovedad>> GetAllWithTipoAsync()
        {
            return await _context.MaestroConceptosNovedad
                .Include(c => c.TipoConcepto)
                .ToListAsync();
        }
    }
}
