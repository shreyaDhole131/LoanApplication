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
    public DbSet<ForeClosureRequest> ForeClosureRequests { get; set; }
    public DbSet<LoanAccount> LoanAccounts { get; set; }
    public DbSet<EmiSchedule> EmiSchedules { get; set; }
    public DbSet<Notification> Notifications { get; set; }





    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


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

    }
    }

