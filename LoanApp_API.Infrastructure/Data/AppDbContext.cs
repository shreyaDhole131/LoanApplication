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

    public DbSet<CibilReport> CibilReports { get; set; } = null!;
    public DbSet<ScoreCard> ScoreCards { get; set; } = null!;
    public DbSet<EligibilityResult> EligibilityResults { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<CibilReport>(c =>
        {
            c.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ScoreCard>(s =>
        {
            s.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<EligibilityResult>(e =>
        {
            e.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });


    }
    }

