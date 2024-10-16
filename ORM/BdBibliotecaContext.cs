using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WEB_API_LIA.ORM;

public partial class BdBibliotecaContext : DbContext
{
    public BdBibliotecaContext()
    {
    }

    public BdBibliotecaContext(DbContextOptions<BdBibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCategoria> TbCategorias { get; set; }

    public virtual DbSet<TbEmprestimo> TbEmprestimos { get; set; }

    public virtual DbSet<TbFuncionario> TbFuncionarios { get; set; }

    public virtual DbSet<TbLivro> TbLivros { get; set; }

    public virtual DbSet<TbMembro> TbMembros { get; set; }

    public virtual DbSet<TbReserva> TbReservas { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB205_15\\SQLEXPRESS;Database=BD_BIBLIOTECA;User Id=LiaCosta;Password=lia123;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCategoria>(entity =>
        {
            entity.ToTable("TB_CATEGORIAS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<TbEmprestimo>(entity =>
        {
            entity.ToTable("TB_EMPRESTIMOS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataDevolucao).HasColumnName("dataDevolucao");
            entity.Property(e => e.DataEmprestimo).HasColumnName("dataEmprestimo");
            entity.Property(e => e.FkLivro).HasColumnName("fkLivro");
            entity.Property(e => e.FkMembro).HasColumnName("fkMembro");

            entity.HasOne(d => d.FkLivroNavigation).WithMany(p => p.TbEmprestimos)
                .HasForeignKey(d => d.FkLivro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_EMPRESTIMOS_TB_LIVROS");

            entity.HasOne(d => d.FkMembroNavigation).WithMany(p => p.TbEmprestimos)
                .HasForeignKey(d => d.FkMembro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_EMPRESTIMOS_TB_MEMBROS");
        });

        modelBuilder.Entity<TbFuncionario>(entity =>
        {
            entity.ToTable("TB_FUNCIONARIOS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<TbLivro>(entity =>
        {
            entity.ToTable("TB_LIVROS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnoPublicacao).HasColumnName("anoPublicacao");
            entity.Property(e => e.Autor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("autor");
            entity.Property(e => e.Disponibilidade).HasColumnName("disponibilidade");
            entity.Property(e => e.FkCategoria).HasColumnName("fkCategoria");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<TbMembro>(entity =>
        {
            entity.ToTable("TB_MEMBROS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataCadastro).HasColumnName("dataCadastro");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefone");
            entity.Property(e => e.TipoMembro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoMembro");
        });

        modelBuilder.Entity<TbReserva>(entity =>
        {
            entity.ToTable("TB_RESERVAS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataReserva).HasColumnName("dataReserva");
            entity.Property(e => e.FkLivro).HasColumnName("fkLivro");
            entity.Property(e => e.FkMembro).HasColumnName("fkMembro");

            entity.HasOne(d => d.FkLivroNavigation).WithMany(p => p.TbReservas)
                .HasForeignKey(d => d.FkLivro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_RESERVAS_TB_LIVROS");

            entity.HasOne(d => d.FkMembroNavigation).WithMany(p => p.TbReservas)
                .HasForeignKey(d => d.FkMembro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_RESERVAS_TB_MEMBROS");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("senha");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
