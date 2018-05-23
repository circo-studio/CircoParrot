using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CircoParrotTools.Data.Models.DB
{
    public partial class CircoParrotContext : DbContext
    {
        public virtual DbSet<FileTransactions> FileTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=rdsparrot.cmfbywl5xqli.us-east-1.rds.amazonaws.com;Database=CircoParrot;User Id=Admin;Password=Password.123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileTransactions>(entity =>
            {
                entity.HasKey(e => e.PurchaseOrderId);

                entity.Property(e => e.PurchaseOrderId)
                    .HasColumnName("PurchaseOrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LineNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });
        }

        public CircoParrotContext(DbContextOptions<CircoParrotContext> options) : base(options)
        {
            //any changes to the context options can now be done here
        }
    }
}
