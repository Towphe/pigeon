using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace repo;

public partial class PigeonContext : DbContext
{
    public PigeonContext()
    {
    }

    public PigeonContext(DbContextOptions<PigeonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<PasswordResetCode> PasswordResetCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("account_pkey");

            entity.ToTable("account");

            entity.HasIndex(e => e.Username, "account_username_key").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_id");
            entity.Property(e => e.Auth0Id)
                .HasMaxLength(255)
                .HasColumnName("auth0_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date_created");
            entity.Property(e => e.UserType)
                .HasMaxLength(10)
                .HasColumnName("user_type");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<PasswordResetCode>(entity =>
        {
            entity.HasKey(e => e.CodeId).HasName("password_reset_codes_pkey");

            entity.ToTable("password_reset_codes");

            entity.Property(e => e.CodeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("code_id");
            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .HasColumnName("code");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CURRENT_TIMESTAMP + '01:00:00'::interval)")
                .HasColumnName("date_created");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
