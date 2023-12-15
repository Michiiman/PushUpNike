using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class EnvioRepository  : GenericRepo<Envio>, IEnvio
{
    protected readonly ApiContext _context;
    public EnvioRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Envio>> GetAllAsync()
    {
        return await _context.Envios
            .ToListAsync();
    }
    public override async Task<Envio> GetByIdAsync(int id)
    {
        return await _context.Envios
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Envio> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Envios as IQueryable<Envio>;
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
