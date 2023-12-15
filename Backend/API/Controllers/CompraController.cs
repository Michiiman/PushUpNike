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

public class CompraController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public CompraController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CompraDto>>> Get()
    {
        var Compra = await unitofwork.Compras.GetAllAsync();
        return mapper.Map<List<CompraDto>>(Compra);
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

    public async Task<ActionResult<CompraDto>> Get(int id)
    {
        var Compra = await unitofwork.Compras.GetByIdAsync(id);
        if (Compra == null){
            return NotFound();
        }
        return this.mapper.Map<CompraDto>(Compra);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Compra>> Post(CompraDto CompraDto)
    {
        var Compra = this.mapper.Map<Compra>(CompraDto);
        this.unitofwork.Compras.Add(Compra);
        await unitofwork.SaveAsync();
        if(Compra == null)
        {
            return BadRequest();
        }
        CompraDto.Id = Compra.Id;
        return CreatedAtAction(nameof(Post), new {id = CompraDto.Id}, CompraDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CompraDto>> Put(int id, [FromBody]CompraDto CompraDto){
        if(CompraDto == null)
        {
            return NotFound();
        }
        var Compra = this.mapper.Map<Compra>(CompraDto);
        unitofwork.Compras.Update(Compra);
        await unitofwork.SaveAsync();
        return CompraDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var Compra = await unitofwork.Compras.GetByIdAsync(id);
        if(Compra == null)
        {
            return NotFound();
        }
        unitofwork.Compras.Remove(Compra);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
