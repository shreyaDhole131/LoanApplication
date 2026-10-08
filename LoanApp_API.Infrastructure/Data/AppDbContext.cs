using LoanApp_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Infrastructure.Data;

public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

    public DbSet<LoanDeals> LoanDeals { get; set; } = null!;
    public DbSet<SanctionLetters> SanctionLetters { get; set; } = null!;
    public DbSet<Disbursements> Disbursements { get; set; } = null!;
    public DbSet<DealReviews> DealReviews { get; set; } = null!;
    public DbSet<Customers> Customers { get; set; } = null!;

    public DbSet<ForeClosureRequest> ForeClosureRequests { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        foreach (var relationship in modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior =
                DeleteBehavior.NoAction;
        }
    

    modelBuilder.Entity<Disbursements>(d =>
        {
            d.HasOne(x => x.deals)
            .WithOne(x => x.disbursement)
            .HasForeignKey<Disbursements>(x => x.DealId)
            .OnDelete(DeleteBehavior.Restrict);
        }
        );
        modelBuilder.Entity<SanctionLetters>(s =>
        {
            s.HasOne(x => x.deals)
            .WithOne(x => x.sanctionLetters)
            .HasForeignKey<SanctionLetters>(x => x.DealId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DealReviews>(d =>
        {
            d.HasOne(x => x.deals)
            .WithOne(x => x.dealReviews)
            .HasForeignKey<DealReviews>(x => x.DealId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DealReviews>(d =>
        {
            d.HasOne(x => x.users)
            .WithMany(x => x.dealReviews)
            .HasForeignKey(x => x.OfficerId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LoanDeals>(d =>
        {
            d.HasOne(x => x.customers)
            .WithMany(x => x.loanDeals)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        });


    }
    }

