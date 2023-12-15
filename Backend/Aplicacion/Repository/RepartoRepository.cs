using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class RepartoRepository  : GenericRepo<Reparto>, IReparto
{
    protected readonly ApiContext _context;
    public RepartoRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Reparto>> GetAllAsync()
    {
        return await _context.Repartos
            .ToListAsync();
    }
    public override async Task<Reparto> GetByIdAsync(int id)
    {
        return await _context.Repartos
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Reparto> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Repartos as IQueryable<Reparto>;
        if(string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Id.ToString().Contains(search));
        }
        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
