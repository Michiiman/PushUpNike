using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize]

public class ProductoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public ProductoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    {
        var Producto = await unitofwork.Productos.GetAllAsync();
        return mapper.Map<List<ProductoDto>>(Producto);
    }
   [HttpGet]
   [MapToApiVersion("1.1")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<Pager<RolDto>>> GetPagination([FromQuery] Params paisParams)
   {
       var entidad = await unitofwork.Roles.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
       var listEntidad = mapper.Map<List<RolDto>>(entidad.registros);
       return new Pager<RolDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
   }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProductoDto>> Get(int id)
    {
        var Producto = await unitofwork.Productos.GetByIdAsync(id);
        if (Producto == null){
            return NotFound();
        }
        return this.mapper.Map<ProductoDto>(Producto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Producto>> Post(ProductoDto ProductoDto)
    {
        var Producto = this.mapper.Map<Producto>(ProductoDto);
        this.unitofwork.Productos.Add(Producto);
        await unitofwork.SaveAsync();
        if(Producto == null)
        {
            return BadRequest();
        }
        ProductoDto.Id = Producto.Id;
        return CreatedAtAction(nameof(Post), new {id = ProductoDto.Id}, ProductoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody]ProductoDto ProductoDto){
        if(ProductoDto == null)
        {
            return NotFound();
        }
        var Producto = this.mapper.Map<Producto>(ProductoDto);
        unitofwork.Productos.Update(Producto);
        await unitofwork.SaveAsync();
        return ProductoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var Producto = await unitofwork.Productos.GetByIdAsync(id);
        if(Producto == null)
        {
            return NotFound();
        }
        unitofwork.Productos.Remove(Producto);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
