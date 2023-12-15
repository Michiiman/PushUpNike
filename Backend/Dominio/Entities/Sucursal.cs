using System.Data.Common;

namespace Dominio.Entities;
public class Sucursal : BaseEntity
{
    public string Nombre { get; set; }
    public string Ciudad { get; set; }
    public string Direccion { get; set; }

    public ICollection<Inventario> Inventarios { get; set; }
    public ICollection<Reparto> Repartos { get; set; }

}
