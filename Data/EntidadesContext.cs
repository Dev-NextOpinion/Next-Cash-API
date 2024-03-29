﻿using API_Financeiro_Next.Models;
using Microsoft.EntityFrameworkCore;


namespace API_Financeiro_Next.Data;

public class EntidadesContext : DbContext
{
    public EntidadesContext(DbContextOptions<EntidadesContext> opts)
        :base (opts)
    {
        
        
    }


    public DbSet<Beneficiarios> Beneficiarios { get; set; }

    public DbSet<Categorias> Categorias { get; set; }

    public DbSet<ContasPagar> Contas { get; set; }

    public DbSet<Receita> Receitas { get; set; }

    public DbSet<Impostos> Impostos { get; set; }

    public DbSet<Orçamentos> Orçamentos { get; set; }

    public DbSet<DespesaFixa> DespesasFixas { get; set; }

    public DbSet<DespesaVariavel> DespesaVariavels { get; set; }

}
