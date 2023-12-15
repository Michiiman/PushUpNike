using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class InventarioRepository  : GenericRepo<Inventario>, IInventario
{
    protected readonly ApiContext _context;
    public InventarioRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Inventario>> GetAllAsync()
    {
        return await _context.Inventarios
            .ToListAsync();
    }
    public override async Task<Inventario> GetByIdAsync(int id)
    {
        return await _context.Inventarios
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Inventario> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Inventarios as IQueryable<Inventario>;
        if(string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Descripcion.ToLower().Contains(search));
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
