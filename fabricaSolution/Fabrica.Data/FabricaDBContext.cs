namespace Fabrica.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FabricaDBContext : IdentityDbContext<FabricaUser>
    {
        public DbSet<Prop> Props { get; set; }
        public DbSet<MarvelousProp> MarvelousProps { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MarvelousPropOrder> MarvelousPropOrders { get; set; }
        public DbSet<PropOrder> PropOrders { get; set; }
        public DbSet<CreditAccount> CreditAccounts { get; set; }

        public FabricaDBContext(DbContextOptions<FabricaDBContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FabricaUser>()
                .HasOne(user => user.CreditAccount)
                .WithOne(account => account.AccountOwner)
                .HasForeignKey<CreditAccount>(account => account.Id);

            builder.Entity<MarvelousPropOrder>()
                .HasKey(mpo => new { mpo.MarvelousPropId, mpo.OrderId});

            builder.Entity<MarvelousPropOrder>()
                .HasOne(x => x.MarvelousProp)
                .WithMany(p => p.Orders)
                .HasForeignKey(x=>x.MarvelousPropId);

            builder.Entity<MarvelousPropOrder>()
                .HasOne(pt => pt.Order)
                .WithMany(t => t.MarvelousProps)
                .HasForeignKey(pt => pt.OrderId);
            

            builder.Entity<PropOrder>()
                .HasKey(mpo => new { mpo.PropId, mpo.OrderId });

            builder.Entity<PropOrder>()
                .HasOne(x => x.Prop)
                .WithMany(p => p.Orders)
                .HasForeignKey(x => x.PropId);

            builder.Entity<PropOrder>()
                .HasOne(pt => pt.Order)
                .WithMany(t => t.Props)
                .HasForeignKey(pt => pt.OrderId);
        }

    }
}
