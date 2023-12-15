using Dominio.Entities;
namespace API.Dtos;
public class ClienteDto : BaseEntity
{
    public int IdUsuarioFk { get; set; }
    public Usuario Usuario { get; set; }
    public string Direccion { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Telefono { get; set; }
    

    public ICollection<Compra> Compras { get; set; }
}
