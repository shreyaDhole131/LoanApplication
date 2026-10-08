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
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    }

