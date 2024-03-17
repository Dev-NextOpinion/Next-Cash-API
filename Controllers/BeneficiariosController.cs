using API_Financeiro_Next.Data;
using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using API_Financeiro_Next.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class BeneficiariosController : ControllerBase
{
    private readonly EntidadesContext _context;
    private readonly IMapper _mapper;
   
    public BeneficiariosController(
        EntidadesContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadastrarBeneficiario([FromBody] 
    CreateBeneficiariosDto createBeneficiariosDto)
    {
        Beneficiarios beneficiarios = _mapper.Map<Beneficiarios>(createBeneficiariosDto);
        _context.Add(beneficiarios);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetBeneficiariosId),
            new { id = beneficiarios.Id }, beneficiarios);
    }

    [HttpGet]
    public IEnumerable<ReadBeneficiariosDto> GetBeneficiarios(
        [FromQuery] string? NomeBeneficiarios = null)
    {
        if(NomeBeneficiarios == null)
        {
            return _mapper.Map<List<ReadBeneficiariosDto>>(
                _context.Beneficiarios.ToList());
        }

        return _mapper.Map<List<ReadBeneficiariosDto>>(_context.Beneficiarios
            .Where(beneficiarios => beneficiarios.ContasAPagar
            .Any(contas => contas.Beneficiarios.NomeBeneficiario == NomeBeneficiarios))
            .ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetBeneficiariosId(int id)
    {
        var beneficiario = _context.Beneficiarios.FirstOrDefault(beneficiario => beneficiario.Id == id);
        if (beneficiario == null) return NotFound();
        var beneficiarioDto = _mapper.Map<ReadBeneficiariosDto>(beneficiario);
        return Ok(beneficiarioDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBeneficiario(int id,
        [FromBody] UpdateBeneficiarioDto updateBeneficiarioDto)
    {
        var beneficiario = _context.Beneficiarios.FirstOrDefault(beneficiario => beneficiario.Id == id);
        if (beneficiario == null) return Ok();

        _mapper.Map(updateBeneficiarioDto, beneficiario);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBeneficiario(int id)
    {
        var beneficiario = _context.Beneficiarios.FirstOrDefault(
            beneficiario => beneficiario.Id == id);

        _context.Remove(beneficiario);
        _context.SaveChanges();
        return NoContent();
    }

}
