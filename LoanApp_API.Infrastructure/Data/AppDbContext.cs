using LoanApp_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LoanApp_API.Domain.Entities;

namespace LoanApp_API.Infrastructure.Data;

public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

    public DbSet<LoanDeals> LoanDeals;

    public DbSet<SanctionLetters> SanctionLetters;

    public DbSet<Disbursements> Disbursements;

    public DbSet<DealReviews> DealReviews;

    public DbSet<ForeClosureRequest> ForeClosureRequests { get; set; }

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
    }
    }

