﻿// <auto-generated />
using System;
using API_Financeiro_Next.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_Financeiro_Next.Migrations
{
    [DbContext(typeof(EntidadesContext))]
    [Migration("20240314133439_ContasPagar")]
    partial class ContasPagar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API_Financeiro_Next.Models.Beneficiarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf_Cnpj")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NomeBeneficiario")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Referencia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Beneficiarios");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.Categorias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TituloCategoria")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.ContasPagar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BeneficiariosId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoriasId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DescricaoDespesa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Fornecedor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Valor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BeneficiariosId");

                    b.HasIndex("CategoriasId");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.DespesaFixa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ReceitaId")
                        .HasColumnType("int");

                    b.Property<string>("TituloDespesaFixa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ValorDespesaFixa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceitaId");

                    b.ToTable("DespesasFixas");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.DespesaVariavel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ReceitaId")
                        .HasColumnType("int");

                    b.Property<string>("TituloDespesaVariavel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ValorDespesaVariavel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceitaId");

                    b.ToTable("DespesaVariavels");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.Impostos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("TipoImposto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TituloImposto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ValorImposto")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Impostos");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.Orçamentos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("TituloOrçamento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Orçamentos");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.Receita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Segmento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TituloProduto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Receitas");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("ImageProfile")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.Categorias", b =>
                {
                    b.HasOne("API_Financeiro_Next.Models.Usuario", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.ContasPagar", b =>
                {
                    b.HasOne("API_Financeiro_Next.Models.Beneficiarios", "Beneficiarios")
                        .WithMany("ContasAPagar")
                        .HasForeignKey("BeneficiariosId");

                    b.HasOne("API_Financeiro_Next.Models.Categorias", "Categorias")
                        .WithMany("ContasAPagar")
                        .HasForeignKey("CategoriasId");

                    b.Navigation("Beneficiarios");

                    b.Navigation("Categorias");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.DespesaFixa", b =>
                {
                    b.HasOne("API_Financeiro_Next.Models.Receita", "Receita")
                        .WithMany("DespesaFixa")
                        .HasForeignKey("ReceitaId");

                    b.Navigation("Receita");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.DespesaVariavel", b =>
                {
                    b.HasOne("API_Financeiro_Next.Models.Receita", "Receita")
                        .WithMany("DespesaVariavel")
                        .HasForeignKey("ReceitaId");

                    b.Navigation("Receita");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.Beneficiarios", b =>
                {
                    b.Navigation("ContasAPagar");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.Categorias", b =>
                {
                    b.Navigation("ContasAPagar");
                });

            modelBuilder.Entity("API_Financeiro_Next.Models.Receita", b =>
                {
                    b.Navigation("DespesaFixa");

                    b.Navigation("DespesaVariavel");
                });
#pragma warning restore 612, 618
        }
    }
}
