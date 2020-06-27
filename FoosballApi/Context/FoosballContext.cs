using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FoosballApi.Context.Entitys;

namespace FoosballApi.Context
{
    public partial class FoosballContext : DbContext
    {
        public FoosballContext()
        {
        }

        public FoosballContext(DbContextOptions<FoosballContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PlayedMatch> PlayedMatch { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<PlayerRating> PlayerRating { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<TeamsRating> TeamsRating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayedMatch>(entity =>
            {
                entity.HasOne(d => d.DefencePlayer1Navigation)
                    .WithMany(p => p.PlayedMatchDefencePlayer1Navigation)
                    .HasForeignKey(d => d.DefencePlayer1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedMatch_DefencePlayer1");

                entity.HasOne(d => d.DefencePlayer2Navigation)
                    .WithMany(p => p.PlayedMatchDefencePlayer2Navigation)
                    .HasForeignKey(d => d.DefencePlayer2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedMatch_DefencePlayer2");

                entity.HasOne(d => d.OffencePlayer1Navigation)
                    .WithMany(p => p.PlayedMatchOffencePlayer1Navigation)
                    .HasForeignKey(d => d.OffencePlayer1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedMatch_OffencePlayer1");

                entity.HasOne(d => d.OffencePlayer2Navigation)
                    .WithMany(p => p.PlayedMatchOffencePlayer2Navigation)
                    .HasForeignKey(d => d.OffencePlayer2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedMatch_OffencePlayer2");

                entity.HasOne(d => d.Team1Navigation)
                    .WithMany(p => p.PlayedMatchTeam1Navigation)
                    .HasForeignKey(d => d.Team1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedMatch_Team1");

                entity.HasOne(d => d.Team2Navigation)
                    .WithMany(p => p.PlayedMatchTeam2Navigation)
                    .HasForeignKey(d => d.Team2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedMatch_Team2");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__tmp_ms_x__737584F60169BD03")
                    .IsUnique();

                entity.Property(e => e.ImageUrl).HasColumnName("imageUrl");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.HasIndex(e => e.DefensePlayerId)
                    .HasName("UQ__Teams__9A460A4D6FC8C749")
                    .IsUnique();

                entity.HasIndex(e => e.OffensePlayerId)
                    .HasName("UQ__Teams__9A460A4A35687EE0")
                    .IsUnique();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.DefensePlayer)
                    .WithOne(p => p.TeamsDefensePlayer)
                    .HasForeignKey<Teams>(d => d.DefensePlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teams_Player2");

                entity.HasOne(d => d.OffensePlayer)
                    .WithOne(p => p.TeamsOffensePlayer)
                    .HasForeignKey<Teams>(d => d.OffensePlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teams_Player1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
