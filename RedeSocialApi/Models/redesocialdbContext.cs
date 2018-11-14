using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RedeSocialApi.Models
{
    public partial class redesocialdbContext : DbContext
    {
        public redesocialdbContext()
        {
        }

        public redesocialdbContext(DbContextOptions<redesocialdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAmizade> TAmizade { get; set; }
        public virtual DbSet<TComentario> TComentario { get; set; }
        public virtual DbSet<THistoria> THistoria { get; set; }
        public virtual DbSet<TLikeDislike> TLikeDislike { get; set; }
        public virtual DbSet<TUsuario> TUsuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=redesocialdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAmizade>(entity =>
            {
                entity.ToTable("t_amizade");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aprovada).HasColumnName("aprovada");

                entity.Property(e => e.Usuario1).HasColumnName("usuario1");

                entity.Property(e => e.Usuario2).HasColumnName("usuario2");

                entity.HasOne(d => d.Usuario1Navigation)
                    .WithMany(p => p.TAmizadeUsuario1Navigation)
                    .HasForeignKey(d => d.Usuario1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_amizade_usuario1");

                entity.HasOne(d => d.Usuario2Navigation)
                    .WithMany(p => p.TAmizadeUsuario2Navigation)
                    .HasForeignKey(d => d.Usuario2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_amizade_usuario2");
            });

            modelBuilder.Entity<TComentario>(entity =>
            {
                entity.ToTable("t_comentario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("date");

                entity.Property(e => e.HistoriaId).HasColumnName("historia_id");

                entity.Property(e => e.Mensagem).HasColumnName("mensagem");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Historia)
                    .WithMany(p => p.TComentario)
                    .HasForeignKey(d => d.HistoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comentario_historia_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TComentario)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comentario_user_id");
            });

            modelBuilder.Entity<THistoria>(entity =>
            {
                entity.ToTable("t_historia");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("date");

                entity.Property(e => e.Foto)
                    .HasColumnName("foto")
                    .IsUnicode(false);

                entity.Property(e => e.Mensagem).HasColumnName("mensagem");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.THistoria)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historia_user_id");
            });

            modelBuilder.Entity<TLikeDislike>(entity =>
            {
                entity.ToTable("t_like_dislike");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HistoriaId).HasColumnName("historia_id");

                entity.Property(e => e.LikeDislike).HasColumnName("like_dislike");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Historia)
                    .WithMany(p => p.TLikeDislike)
                    .HasForeignKey(d => d.HistoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_like_dislike_historia_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TLikeDislike)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_like_dislike_user_id");
            });

            modelBuilder.Entity<TUsuario>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("t_usuario");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FotoPerfil)
                    .HasColumnName("foto_perfil")
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasColumnName("senha")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
