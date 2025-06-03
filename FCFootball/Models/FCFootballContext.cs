using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FCFootball.Models
{
    public partial class FCFootballContext : DbContext
    {
        public FCFootballContext()
        {
        }

        public FCFootballContext(DbContextOptions<FCFootballContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Game { get; set; } = null!;
        public virtual DbSet<GameStat> GameStats { get; set; } = null!;
        public virtual DbSet<Player> Player { get; set; } = null!;
        public virtual DbSet<PlayerStat> PlayerStats { get; set; } = null!;
        public virtual DbSet<Staff> Staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=FCFootballConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");

                entity.Property(e => e.GameID).HasColumnName("GameID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Opponent).HasMaxLength(50);

                entity.Property(e => e.Result)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GameStat>(entity =>
            {
                entity.Property(e => e.GameStatID).HasColumnName("GameStatID");

                entity.Property(e => e.GameID).HasColumnName("GameID");

                entity.Property(e => e.PassTd).HasColumnName("PassTD");

                entity.Property(e => e.RecTd).HasColumnName("RecTD");

                entity.Property(e => e.RushTd).HasColumnName("RushTD");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameStats)
                    .HasForeignKey(d => d.GameID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GameStats_Games");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.Property(e => e.PlayerID).HasColumnName("PlayerID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Height)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Position)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<PlayerStat>(entity =>
            {
                entity.Property(e => e.PlayerStatID).HasColumnName("PlayerStatID");

                entity.Property(e => e.GameID).HasColumnName("GameID");

                entity.Property(e => e.PassTd).HasColumnName("PassTD");

                entity.Property(e => e.PlayerID).HasColumnName("PlayerID");

                entity.Property(e => e.RecTd).HasColumnName("RecTD");

                entity.Property(e => e.RushTd).HasColumnName("RushTD");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.PlayerStats)
                    .HasForeignKey(d => d.GameID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PlayerStats_Games");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerStats)
                    .HasForeignKey(d => d.PlayerID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PlayerStats_Players");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffID).HasColumnName("StaffID");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(10);

                entity.Property(e => e.Role).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
