using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AdventureWK1.Model;

public partial class AdventureWorks2019Context : DbContext
{
    public AdventureWorks2019Context()
    {
    }

    public AdventureWorks2019Context(DbContextOptions<AdventureWorks2019Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CreditCard> CreditCards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ADITYA\\SQLEXPRESS;Database=AdventureWorks2019;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.HasKey(e => e.CreditCardId).HasName("PK_CreditCard_CreditCardID");

            entity.ToTable("CreditCard", "Sales", tb => tb.HasComment("Customer credit card information."));

            entity.HasIndex(e => e.CardNumber, "AK_CreditCard_CardNumber").IsUnique();

            entity.Property(e => e.CreditCardId)
                .HasComment("Primary key for CreditCard records.")
                .HasColumnName("CreditCardID");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(25)
                .HasComment("Credit card number.");
            entity.Property(e => e.CardType)
                .HasMaxLength(50)
                .HasComment("Credit card name.");
            entity.Property(e => e.ExpMonth).HasComment("Credit card expiration month.");
            entity.Property(e => e.ExpYear).HasComment("Credit card expiration year.");
            entity.Property(e => e.ModifiedDate)
                .HasComment("Date and time the record was last updated.")
                .HasDefaultValueSql("(getdate())", "DF_CreditCard_ModifiedDate")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
