﻿using BankServices.Client.Models;
using Microsoft.EntityFrameworkCore;

namespace BankServices.Client.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options)
            : base(options)
        {

        }

        public DbSet<BankAccountModel> BankAccounts { get; set; }
    }
}
