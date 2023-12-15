using Dominio.Entities;
namespace API.Dtos;
public class CategoriaDto : BaseEntity
{
    public string Nombre { get; set; }

    public ICollection<Producto> Productos { get; set; }
}
