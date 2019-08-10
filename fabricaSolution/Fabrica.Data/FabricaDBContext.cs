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
        public DbSet<Licenze> Licenzes { get; set; }

        public FabricaDBContext(DbContextOptions<FabricaDBContext> options) : base(options)
        {
                
        }

        public FabricaDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //User Account
            builder.Entity<FabricaUser>()
                .HasOne(user => user.CreditAccount)
                .WithOne(account => account.AccountOwner)
                .HasForeignKey<CreditAccount>(account => account.AccountOwnerId);

            //MarvelousProps Orders
            builder.Entity<MarvelousPropOrder>()
                .HasKey(order => new { order.MarvelousPropId, order.OrderId});

            builder.Entity<MarvelousPropOrder>()
                .HasOne(o => o.MarvelousProp)
                .WithMany(m => m.Orders)
                .HasForeignKey(o=>o.MarvelousPropId);

            builder.Entity<MarvelousPropOrder>()
                .HasOne(m => m.Order)
                .WithMany(o => o.MarvelousProps)
                .HasForeignKey(m => m.OrderId);
            
            //Prop Orders
            builder.Entity<PropOrder>()
                .HasKey(order => new { order.PropId, order.OrderId });

            builder.Entity<PropOrder>()
                .HasOne(o => o.Prop)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PropId);

            builder.Entity<PropOrder>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Props)
                .HasForeignKey(p => p.OrderId);
        }

    }
}
