using API_Financeiro_Next.Data;
using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class DespesasFixasController : ControllerBase
{
    private EntidadesContext _context;
    private IMapper _mapper;

    public DespesasFixasController(EntidadesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadastrarDespesaFixa([FromBody]
    CreateDespesaFixaDto createDespesaFixaDto)
    {
        DespesaFixa despesa = _mapper.Map<DespesaFixa>(createDespesaFixaDto);
        _context.Add(despesa);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetDespesasFixasId),
            new { id = despesa.Id }, despesa);
    }

    [HttpGet]
    public IEnumerable<ReadDespesaFixaDto> GetDespesasFixas()
    {
        return _mapper.Map<List<ReadDespesaFixaDto>>(
            _context.DespesasFixas.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetDespesasFixasId(int id)
    {
        var despesas = _context.DespesasFixas.FirstOrDefault(
            despesas => despesas.Id == id);
        if (despesas == null) NotFound();
        var despesasDto = _mapper.Map<ReadDespesaFixaDto>(despesas);
        return Ok(despesasDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDespesaFixa(int id,
      [FromBody] UpdateDespesaFixaDto updateDespesaFixaDto)
    {
        var despesa = _context.DespesasFixas.FirstOrDefault(
            despesa => despesa.Id == id);
        if (despesa == null) NotFound();

        _mapper.Map(updateDespesaFixaDto, despesa);
        _context.SaveChanges();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteDespesaFixa(int id)
    {
        var despesa = _context.DespesasFixas.FirstOrDefault(
            despesa => despesa.Id == id);
        if (despesa == null) NotFound();

        _context.Remove(despesa);
        _context.SaveChanges();
        return NoContent();
    }


}
