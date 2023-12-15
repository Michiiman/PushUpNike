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

public class ClienteController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public ClienteController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var Cliente = await unitofwork.Clientes.GetAllAsync();
        return mapper.Map<List<ClienteDto>>(Cliente);
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

    public async Task<ActionResult<ClienteDto>> Get(int id)
    {
        var Cliente = await unitofwork.Clientes.GetByIdAsync(id);
        if (Cliente == null){
            return NotFound();
        }
        return this.mapper.Map<ClienteDto>(Cliente);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Cliente>> Post(ClienteDto ClienteDto)
    {
        var Cliente = this.mapper.Map<Cliente>(ClienteDto);
        this.unitofwork.Clientes.Add(Cliente);
        await unitofwork.SaveAsync();
        if(Cliente == null)
        {
            return BadRequest();
        }
        ClienteDto.Id = Cliente.Id;
        return CreatedAtAction(nameof(Post), new {id = ClienteDto.Id}, ClienteDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody]ClienteDto ClienteDto){
        if(ClienteDto == null)
        {
            return NotFound();
        }
        var Cliente = this.mapper.Map<Cliente>(ClienteDto);
        unitofwork.Clientes.Update(Cliente);
        await unitofwork.SaveAsync();
        return ClienteDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var Cliente = await unitofwork.Clientes.GetByIdAsync(id);
        if(Cliente == null)
        {
            return NotFound();
        }
        unitofwork.Clientes.Remove(Cliente);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
