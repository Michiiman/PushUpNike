using API.Dtos;
using AutoMapper;
using Dominio.Entities;
namespace API.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    { 
       CreateMap<Rol,RolDto>().ReverseMap();
       CreateMap<Usuario,UsuarioDto>().ReverseMap();
       CreateMap<Cliente,ClienteDto>().ReverseMap();
       CreateMap<Sucursal,SucursalDto>().ReverseMap();
       CreateMap<Reparto,RepartoDto>().ReverseMap();
       CreateMap<Compra,CompraDto>().ReverseMap();
       CreateMap<Inventario,InventarioDto>().ReverseMap();
       CreateMap<Producto,ProductoDto>().ReverseMap();
       CreateMap<Categoria,CategoriaDto>().ReverseMap();
       CreateMap<Envio,EnvioDto>().ReverseMap();
   }
}
