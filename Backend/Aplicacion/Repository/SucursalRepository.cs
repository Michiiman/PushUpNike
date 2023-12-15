using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class SucursalRepository  : GenericRepo<Sucursal>, ISucursal
{
    protected readonly ApiContext _context;
    public SucursalRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Sucursal>> GetAllAsync()
    {
        return await _context.Sucursales
            .ToListAsync();
    }
    public override async Task<Sucursal> GetByIdAsync(int id)
    {
        return await _context.Sucursales
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Sucursal> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Sucursales as IQueryable<Sucursal>;
        if(string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
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
