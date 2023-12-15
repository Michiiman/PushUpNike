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

public class EnvioController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public EnvioController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<EnvioDto>>> Get()
    {
        var Envio = await unitofwork.Envios.GetAllAsync();
        return mapper.Map<List<EnvioDto>>(Envio);
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

    public async Task<ActionResult<EnvioDto>> Get(int id)
    {
        var Envio = await unitofwork.Envios.GetByIdAsync(id);
        if (Envio == null){
            return NotFound();
        }
        return this.mapper.Map<EnvioDto>(Envio);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Envio>> Post(EnvioDto EnvioDto)
    {
        var Envio = this.mapper.Map<Envio>(EnvioDto);
        this.unitofwork.Envios.Add(Envio);
        await unitofwork.SaveAsync();
        if(Envio == null)
        {
            return BadRequest();
        }
        EnvioDto.Id = Envio.Id;
        return CreatedAtAction(nameof(Post), new {id = EnvioDto.Id}, EnvioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EnvioDto>> Put(int id, [FromBody]EnvioDto EnvioDto){
        if(EnvioDto == null)
        {
            return NotFound();
        }
        var Envio = this.mapper.Map<Envio>(EnvioDto);
        unitofwork.Envios.Update(Envio);
        await unitofwork.SaveAsync();
        return EnvioDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var Envio = await unitofwork.Envios.GetByIdAsync(id);
        if(Envio == null)
        {
            return NotFound();
        }
        unitofwork.Envios.Remove(Envio);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
