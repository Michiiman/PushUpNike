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

public class SucursalController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public SucursalController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<SucursalDto>>> Get()
    {
        var Sucursal = await unitofwork.Sucursals.GetAllAsync();
        return mapper.Map<List<SucursalDto>>(Sucursal);
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

    public async Task<ActionResult<SucursalDto>> Get(int id)
    {
        var Sucursal = await unitofwork.Sucursals.GetByIdAsync(id);
        if (Sucursal == null){
            return NotFound();
        }
        return this.mapper.Map<SucursalDto>(Sucursal);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Sucursal>> Post(SucursalDto SucursalDto)
    {
        var Sucursal = this.mapper.Map<Sucursal>(SucursalDto);
        this.unitofwork.Sucursals.Add(Sucursal);
        await unitofwork.SaveAsync();
        if(Sucursal == null)
        {
            return BadRequest();
        }
        SucursalDto.Id = Sucursal.Id;
        return CreatedAtAction(nameof(Post), new {id = SucursalDto.Id}, SucursalDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<SucursalDto>> Put(int id, [FromBody]SucursalDto SucursalDto){
        if(SucursalDto == null)
        {
            return NotFound();
        }
        var Sucursal = this.mapper.Map<Sucursal>(SucursalDto);
        unitofwork.Sucursals.Update(Sucursal);
        await unitofwork.SaveAsync();
        return SucursalDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var Sucursal = await unitofwork.Sucursals.GetByIdAsync(id);
        if(Sucursal == null)
        {
            return NotFound();
        }
        unitofwork.Sucursals.Remove(Sucursal);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
