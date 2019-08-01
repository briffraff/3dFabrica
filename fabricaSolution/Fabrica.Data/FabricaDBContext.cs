﻿namespace Fabrica.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FabricaDBContext : IdentityDbContext<FabricaUser>
    {
        public DbSet<Prop> Props { get; set; }
        public DbSet<MarvelousProp> MarvelousProps { get; set; }
        public DbSet<Order> Orders { get; set; }

        public FabricaDBContext(DbContextOptions<FabricaDBContext> options) : base(options)
        {

        }

    }
}
