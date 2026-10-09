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

    public DbSet<LoanPayment> LoanPayment { get; set; } = null!;

    public DbSet<SupportTicket> SupportTickets { get; set; } = null!;
    public DbSet<LoanDeals> LoanDeals { get; set; } = null!;
    public DbSet<SanctionLetters> SanctionLetters { get; set; } = null!;
    public DbSet<Disbursements> Disbursements { get; set; } = null!;
    public DbSet<DealReviews> DealReviews { get; set; } = null!;
    public DbSet<Customers> Customers { get; set; } = null!;
    public DbSet<KycDocument> KycDocuments { get; set; } = null!;
    public DbSet<LoanAccount> LoanAccounts { get; set; }
    public DbSet<EmiSchedule> EmiSchedules { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ForeClosureRequest> ForeClosureRequests { get; set; } = null!; 
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
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

        // FORECLOSURE
        modelBuilder.Entity<ForeClosureRequest>(entity =>
        {
            // Pk
            entity.HasKey(x => x.RequestId);

            // Money columns: 18 digits total, 2 after the decimal point
            entity.Property(x => x.ForeClosureAmount).HasColumnType("decimal(18,2)");
            entity.Property(x => x.PartialAmount).HasColumnType("decimal(18,2)");

            // One loan account can have many foreclosure requests
            entity.HasOne(x => x.LoanAccount)
                  .WithMany()
                  .HasForeignKey(x => x.LoanAccountId);
        });




        modelBuilder.Entity<LoanAccount>()
            .HasOne(la => la.LoanDeal)
            .WithMany(ld => ld.LoanAccounts)
            .HasForeignKey(la => la.DealId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<LoanAccount>()
            .HasOne(la => la.Customer)
            .WithMany(c => c.LoanAccounts)
            .HasForeignKey(la => la.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmiSchedule>()
            .HasOne(es => es.LoanAccount)
            .WithMany(la => la.EmiSchedules)
            .HasForeignKey(es => es.LoanAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notification>()
           .HasOne(n => n.Customer)
           .WithMany(c => c.Notifications)
           .HasForeignKey(n => n.CustomerId)
           .OnDelete(DeleteBehavior.Restrict);

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
            .WithMany(x => x.DealReviews)
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

        modelBuilder.Entity<KycDocument>(k =>
        {
            k.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<SupportTicket>(s =>
        {
            s.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            s.HasOne(x => x.LoanAccount)
                .WithMany()
                .HasForeignKey(x => x.LoanAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
    }

