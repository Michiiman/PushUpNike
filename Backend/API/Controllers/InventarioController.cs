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

public class InventarioController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public InventarioController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<InventarioDto>>> Get()
    {
        var Inventario = await unitofwork.Inventarios.GetAllAsync();
        return mapper.Map<List<InventarioDto>>(Inventario);
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

    public async Task<ActionResult<InventarioDto>> Get(int id)
    {
        var Inventario = await unitofwork.Inventarios.GetByIdAsync(id);
        if (Inventario == null){
            return NotFound();
        }
        return this.mapper.Map<InventarioDto>(Inventario);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Inventario>> Post(InventarioDto InventarioDto)
    {
        var Inventario = this.mapper.Map<Inventario>(InventarioDto);
        this.unitofwork.Inventarios.Add(Inventario);
        await unitofwork.SaveAsync();
        if(Inventario == null)
        {
            return BadRequest();
        }
        InventarioDto.Id = Inventario.Id;
        return CreatedAtAction(nameof(Post), new {id = InventarioDto.Id}, InventarioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<InventarioDto>> Put(int id, [FromBody]InventarioDto InventarioDto){
        if(InventarioDto == null)
        {
            return NotFound();
        }
        var Inventario = this.mapper.Map<Inventario>(InventarioDto);
        unitofwork.Inventarios.Update(Inventario);
        await unitofwork.SaveAsync();
        return InventarioDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var Inventario = await unitofwork.Inventarios.GetByIdAsync(id);
        if(Inventario == null)
        {
            return NotFound();
        }
        unitofwork.Inventarios.Remove(Inventario);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
