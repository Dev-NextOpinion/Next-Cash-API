using API_Financeiro_Next.Data;
using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class DespesasVariaveisController : ControllerBase
{
    private EntidadesContext _context;
    private IMapper _mapper;

    public DespesasVariaveisController(EntidadesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadastrarDespesaVariavel([FromBody]
    CreateDespesaVariavelDto createDespesaVariavelDto)
    {
        DespesaVariavel despesa = _mapper.Map<DespesaVariavel>(createDespesaVariavelDto);
        _context.Add(despesa);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetDespesasVariaveisId),
            new { id = despesa.Id }, despesa);
    }

    [HttpGet]
    public IEnumerable<ReadDespesaVariavelDto> GetDespesasVariaveis()
    {
        return _mapper.Map<List<ReadDespesaVariavelDto>>(
            _context.DespesaVariavels.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetDespesasVariaveisId(int id)
    {
        var despesas = _context.DespesaVariavels.FirstOrDefault(
            despesas => despesas.Id == id);
        if (despesas == null) NotFound();
        var despesasDto = _mapper.Map<ReadDespesaVariavelDto>(despesas);
        return Ok(despesasDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDespesaVariavel(int id,
      [FromBody] UpdateDespesaVariavelDto updateDespesavariavelDto)
    {
        var despesa = _context.DespesaVariavels.FirstOrDefault(
            despesa => despesa.Id == id);
        if (despesa == null) NotFound();

        _mapper.Map(updateDespesavariavelDto, despesa);
        _context.SaveChanges();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteDespesaVariavel(int id)
    {
        var despesa = _context.DespesaVariavels.FirstOrDefault(
            despesa => despesa.Id == id);
        if (despesa == null) NotFound();

        _context.Remove(despesa);
        _context.SaveChanges();
        return NoContent();
    }
}
