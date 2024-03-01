using API_Financeiro_Next.Data;
using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class ReceitasController : ControllerBase
{
    private EntidadesContext _context;
    private IMapper _mapper;

    public ReceitasController(EntidadesContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadastrarReceita([FromBody]
    CreateReceitaDto createReceitaDto)
    {
        Receita receita = _mapper.Map<Receita>(createReceitaDto);
        _context.Add(receita);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetReceitaId),
            new { id = receita.Id }, receita);
    }

    [HttpGet]
    public IEnumerable<ReadReceitaDto> GetReceita()
    {
        return _mapper.Map<List<ReadReceitaDto>>(
            _context.Receitas.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetReceitaId(int id)
    {
        var receita = _context.Receitas.FirstOrDefault(
            receita => receita.Id == id);
        if (receita == null) NotFound();
        var receitaDto = _mapper.Map<ReadReceitaDto>(receita);
        return Ok(receitaDto);

    }

    [HttpPut("{id}")]
    public IActionResult UpdateReceita(int id,
       [FromBody] UpdateReceitaDto updateReceitaDto)
    {
        var receita = _context.Receitas.FirstOrDefault(
            receita => receita.Id == id);
        if (receita == null) NotFound();

        _mapper.Map(updateReceitaDto, receita);
        _context.SaveChanges();
        return NoContent();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReceita(int id)
    {
        var receita = _context.Receitas.FirstOrDefault(
            receita => receita.Id == id);
        if (receita == null) NotFound();

        _context.Remove(receita);
        _context.SaveChanges();
        return NoContent();
    }
    

}
