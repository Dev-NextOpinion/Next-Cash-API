using API_Financeiro_Next.Data;
using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{
    private EntidadesContext _context;
    private IMapper _mapper;

    public CategoriasController(EntidadesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpPost]
    public IActionResult CadastrarCategoria([FromBody] 
    CreateCategoriaDto createCategoriaDto)
    {
        Categorias categoria = _mapper.Map<Categorias>(createCategoriaDto);
        _context.Add(categoria);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCategoriaId),
            new {id = categoria.Id }, categoria);
    }

    [HttpGet]
    public IEnumerable<ReadCategoriasDto> GetCategorias()
    {
        return _mapper.Map<List<ReadCategoriasDto>>(
            _context.Categorias.ToList());

    }


    [HttpGet("{id}")]
    public IActionResult GetCategoriaId(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(
            categoria =>  categoria.Id == id);
        if (categoria == null) NotFound();
        var categoriaDto = _mapper.Map<ReadCategoriasDto>(categoria);
        return Ok(categoriaDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategoria(int id,
        [FromBody] UpdateCategoriasDto updateCategoriasDto)
    {
        var categoria = _context.Categorias.FirstOrDefault(
            categoria => categoria.Id == id);
        if (categoria == null) NotFound();

        _mapper.Map(updateCategoriasDto, categoria);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategoria(int id)
    {
        var categoria = _context.Categorias.FirstOrDefaultAsync(
            categoria => categoria.Id == id);
        if (categoria == null) NotFound();

        _context.Remove(categoria);
        _context.SaveChanges();
        return NoContent();
    }
}
