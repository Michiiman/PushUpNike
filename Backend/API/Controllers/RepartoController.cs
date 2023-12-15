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

public class RepartoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public RepartoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<RepartoDto>>> Get()
    {
        var Reparto = await unitofwork.Repartos.GetAllAsync();
        return mapper.Map<List<RepartoDto>>(Reparto);
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

    public async Task<ActionResult<RepartoDto>> Get(int id)
    {
        var Reparto = await unitofwork.Repartos.GetByIdAsync(id);
        if (Reparto == null){
            return NotFound();
        }
        return this.mapper.Map<RepartoDto>(Reparto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Reparto>> Post(RepartoDto RepartoDto)
    {
        var Reparto = this.mapper.Map<Reparto>(RepartoDto);
        this.unitofwork.Repartos.Add(Reparto);
        await unitofwork.SaveAsync();
        if(Reparto == null)
        {
            return BadRequest();
        }
        RepartoDto.Id = Reparto.Id;
        return CreatedAtAction(nameof(Post), new {id = RepartoDto.Id}, RepartoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RepartoDto>> Put(int id, [FromBody]RepartoDto RepartoDto){
        if(RepartoDto == null)
        {
            return NotFound();
        }
        var Reparto = this.mapper.Map<Reparto>(RepartoDto);
        unitofwork.Repartos.Update(Reparto);
        await unitofwork.SaveAsync();
        return RepartoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var Reparto = await unitofwork.Repartos.GetByIdAsync(id);
        if(Reparto == null)
        {
            return NotFound();
        }
        unitofwork.Repartos.Remove(Reparto);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
