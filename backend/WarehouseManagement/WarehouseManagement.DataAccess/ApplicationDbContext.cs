using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.DataAccess;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { Database.EnsureCreated(); }

    public DbSet<Resource> Resources { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<ReceiptDocument> ReceiptDocuments { get; set; }
    public DbSet<ReceiptResource> ReceiptResources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(r => r.Id);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(u => u.Id);
        });

        modelBuilder.Entity<ReceiptDocument>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.HasMany(d => d.ReceiptResources)
                .WithOne(rr => rr.ReceiptDocument)
                .HasForeignKey(rr => rr.ReceiptDocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ReceiptResource>(entity =>
        {
            entity.HasKey(rr => rr.Id);

            entity.HasOne(rr => rr.Resource)
                .WithMany()
                .HasForeignKey(rr => rr.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(rr => rr.Unit)
                .WithMany()
                .HasForeignKey(rr => rr.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(rr => new { rr.ReceiptDocumentId, rr.ResourceId, rr.UnitId });
        });
    }
}
