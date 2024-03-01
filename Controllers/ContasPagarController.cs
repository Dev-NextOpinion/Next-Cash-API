using API_Financeiro_Next.Data;
using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class ContasPagarController : ControllerBase
{
    private EntidadesContext _context;
    private IMapper _mapper;

    public ContasPagarController(EntidadesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadatrarContasPagar([FromBody] 
    CreateContasPagarDto createContasPagarDto)
    {
        ContasPagar contas = _mapper.Map<ContasPagar>(createContasPagarDto);
        _context.Add(contas);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetContasPagarId),
            new { id = contas.Id }, contas);
    }

    [HttpGet]
    public IEnumerable<ReadContasPagarDto> GetContasPagar(
        [FromQuery] string? contaPagar = null)
    {
        if(contaPagar == null)
        {
            return _mapper.Map<List<ReadContasPagarDto>>(
            _context.Contas.ToList());

        }
        // Consulta a conta através do nome do fornecedor
        return _mapper.Map<List<ReadContasPagarDto>>(_context.Contas
            .Where(conta => conta.Fornecedor.Contains(contaPagar)).ToList());
        
    }

    [HttpGet("{id}")]
    public IActionResult GetContasPagarId(int id)
    {
        var contas = _context.Contas.FirstOrDefault(
            contas => contas.Id == id);
        if (contas == null) NotFound();
        var contasDto = _mapper.Map<ReadContasPagarDto>(contas);
        return Ok(contasDto);            
    }

    [HttpPut("{id}")]
    public IActionResult UpdateContasPagar(int id, 
        [FromBody] UpdateContasPagarDto updateContasPagarDto)
    {
        var categoria = _context.Contas.FirstOrDefault(
            categoria => categoria.Id == id);
        if (categoria == null) NotFound();

        _mapper.Map(updateContasPagarDto, categoria);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteContasPagar(int id)
    {
        var contas = _context.Contas.FirstOrDefault(
            contas => contas.Id == id);
        if (contas == null) NotFound();

        _context.Remove(contas);
        _context.SaveChanges();
        return NoContent();
    }


}
