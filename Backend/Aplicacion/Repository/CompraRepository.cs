using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class CompraRepository  : GenericRepo<Compra>, ICompra
{
    protected readonly ApiContext _context;
    public CompraRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Compra>> GetAllAsync()
    {
        return await _context.Compras
            .ToListAsync();
    }
    public override async Task<Compra> GetByIdAsync(int id)
    {
        return await _context.Compras
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Compra> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Compras as IQueryable<Compra>;
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
