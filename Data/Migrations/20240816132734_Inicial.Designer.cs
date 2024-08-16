﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WS.WorkerExample.Data;

#nullable disable

namespace WS.WorkerExample.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240816132734_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.CentroDeCusto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("ResponsavelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ResponsavelId")
                        .IsUnique()
                        .HasFilter("[ResponsavelId] IS NOT NULL");

                    b.ToTable("CentrosDeCusto");
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.Contato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FuncionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Contatos");
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FuncionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.Funcionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CentroDeCustoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CentroDeCustoId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.CentroDeCusto", b =>
                {
                    b.HasOne("WS.WorkerExample.Data.Entities.Funcionario", "Responsaveis")
                        .WithOne()
                        .HasForeignKey("WS.WorkerExample.Data.Entities.CentroDeCusto", "ResponsavelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Responsaveis");
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.Contato", b =>
                {
                    b.HasOne("WS.WorkerExample.Data.Entities.Empresa", null)
                        .WithMany("Contatos")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WS.WorkerExample.Data.Entities.Funcionario", null)
                        .WithMany("Contatos")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.Endereco", b =>
                {
                    b.HasOne("WS.WorkerExample.Data.Entities.Empresa", null)
                        .WithMany("Enderecos")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WS.WorkerExample.Data.Entities.Funcionario", null)
                        .WithMany("Enderecos")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.Funcionario", b =>
                {
                    b.HasOne("WS.WorkerExample.Data.Entities.CentroDeCusto", "CentroDeCustos")
                        .WithMany("Funcionarios")
                        .HasForeignKey("CentroDeCustoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("CentroDeCustos");
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.CentroDeCusto", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.Empresa", b =>
                {
                    b.Navigation("Contatos");

                    b.Navigation("Enderecos");
                });

            modelBuilder.Entity("WS.WorkerExample.Data.Entities.Funcionario", b =>
                {
                    b.Navigation("Contatos");

                    b.Navigation("Enderecos");
                });
#pragma warning restore 612, 618
        }
    }
}
