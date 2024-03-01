using API_Financeiro_Next.Data;
using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using API_Financeiro_Next.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly EntidadesContext _context;
    private readonly IMapper _mapper;
    private readonly CategoriaService _categoriaService;

    public CategoriasController(EntidadesContext context, 
        IMapper mapper, CategoriaService categoriaService)
    {
        _context = context;
        _mapper = mapper;
        _categoriaService = categoriaService;
    }

    [HttpPost]
    [Authorize("AuthenticationUser")]
    public async Task<IActionResult> CriarCategoria([FromBody] 
    CreateCategoriaDto categoriaDto)
    {
        try
        {
            // Obtenha o Id do usuário autenticado
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _categoriaService.CriarCategoria(categoriaDto, userId);
            return Ok("Categoria criada com sucesso");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet]
    public IEnumerable<ReadCategoriasDto> GetCategorias(
        [FromQuery] string? tituloCategoria = null)
    {
        if(tituloCategoria == null)
        {
            return _mapper.Map<List<ReadCategoriasDto>>(
            _context.Categorias.ToList());
        }
        
        // Se o titulo não for nulo
        // Where -> busca contas a pagar de cada categoria
        // Any -> Verifica se essa conta tem uma categoria, se sim busca o nome atribuido na variável
        return _mapper.Map<List<ReadCategoriasDto>>(_context.Categorias
            .Where(categoria => categoria.ContasAPagar
            .Any(contas => contas.Categorias.TituloCategoria == tituloCategoria))
            .ToList());

    }

    [HttpGet("{id}")]
    public IActionResult GetCategoriaId(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();
        var categoriaDto = _mapper.Map<ReadCategoriasDto>(categoria);
        return Ok(categoriaDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategoria(int id,
        [FromBody] UpdateCategoriasDto updateCategoriasDto)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        _mapper.Map(updateCategoriasDto, categoria);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategoria(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        _context.Remove(categoria);
        _context.SaveChanges();
        return NoContent();
    }
}
