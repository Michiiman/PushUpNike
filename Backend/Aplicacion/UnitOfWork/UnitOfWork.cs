using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;
public class UnitOfWork  : IUnitOfWork, IDisposable
{
   public UnitOfWork(ApiContext context)
   {
       _context = context;
   }
   private readonly ApiContext _context;
   private RolRepository _Rol;
   public IRol Roles
   {
       get{
           if(_Rol== null)
           {
               _Rol= new RolRepository(_context);
           }
           return _Rol;
       }
   }
   private UsuarioRepository _usuarios;
   public IUsuario Usuarios
   {
       get{
           if(_usuarios== null)
           {
               _usuarios= new UsuarioRepository(_context);
           }
           return _usuarios;
       }
   }
   private ClienteRepository _Clientes;
   public ICliente Clientes
   {
       get{
           if(_Clientes== null)
           {
               _Clientes= new ClienteRepository(_context);
           }
           return _Clientes;
       }
   }
   private SucursalRepository _Sucursals;
   public ISucursal Sucursals
   {
       get{
           if(_Sucursals== null)
           {
               _Sucursals= new SucursalRepository(_context);
           }
           return _Sucursals;
       }
   }
   private RepartoRepository _Repartos;
   public IReparto Repartos
   {
       get{
           if(_Repartos== null)
           {
               _Repartos= new RepartoRepository(_context);
           }
           return _Repartos;
       }
   }
   private CompraRepository _Compras;
   public ICompra Compras
   {
       get{
           if(_Compras== null)
           {
               _Compras= new CompraRepository(_context);
           }
           return _Compras;
       }
   }
   private InventarioRepository _Inventarios;
   public IInventario Inventarios
   {
       get{
           if(_Inventarios== null)
           {
               _Inventarios= new InventarioRepository(_context);
           }
           return _Inventarios;
       }
   }
   private ProductoRepository _Productos;
   public IProducto Productos
   {
       get{
           if(_Productos== null)
           {
               _Productos= new ProductoRepository(_context);
           }
           return _Productos;
       }
   }
   private CategoriaRepository _Categorias;
   public ICategoria Categorias
   {
       get{
           if(_Categorias== null)
           {
               _Categorias= new CategoriaRepository(_context);
           }
           return _Categorias;
       }
   }
   private EnvioRepository _Envios;
   public IEnvio Envios
   {
       get{
           if(_Envios== null)
           {
               _Envios= new EnvioRepository(_context);
           }
           return _Envios;
       }
   }
   public void Dispose()
   {
       _context.Dispose();
   }
   public async Task<int> SaveAsync()
   {
       return await _context.SaveChangesAsync();
   } 
   } 
