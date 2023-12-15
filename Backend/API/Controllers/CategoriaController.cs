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
//[Authorize]

public class CategoriaController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public CategoriaController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CategoriaDto>>> Get()
    {
        var Categoria = await unitofwork.Categorias.GetAllAsync();
        return mapper.Map<List<CategoriaDto>>(Categoria);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CategoriaDto>>> GetPagination([FromQuery] Params paisParams)
    {
        var entidad = await unitofwork.Roles.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = mapper.Map<List<CategoriaDto>>(entidad.registros);
        return new Pager<CategoriaDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CategoriaDto>> Get(int id)
    {
        var Categoria = await unitofwork.Categorias.GetByIdAsync(id);
        if (Categoria == null)
        {
            return NotFound();
        }
        return this.mapper.Map<CategoriaDto>(Categoria);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Categoria>> Post(CategoriaDto CategoriaDto)
    {
        var Categoria = this.mapper.Map<Categoria>(CategoriaDto);
        this.unitofwork.Categorias.Add(Categoria);
        await unitofwork.SaveAsync();
        if (Categoria == null)
        {
            return BadRequest();
        }
        CategoriaDto.Id = Categoria.Id;
        return CreatedAtAction(nameof(Post), new { id = CategoriaDto.Id }, CategoriaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CategoriaDto>> Put(int id, [FromBody] CategoriaDto CategoriaDto)
    {
        if (CategoriaDto == null)
        {
            return NotFound();
        }
        var Categoria = this.mapper.Map<Categoria>(CategoriaDto);
        unitofwork.Categorias.Update(Categoria);
        await unitofwork.SaveAsync();
        return CategoriaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Categoria = await unitofwork.Categorias.GetByIdAsync(id);
        if (Categoria == null)
        {
            return NotFound();
        }
        unitofwork.Categorias.Remove(Categoria);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
