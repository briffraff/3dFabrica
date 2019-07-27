namespace Fabrica.Data
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FabricaDBContext : IdentityDbContext<FabricaUser>
    {
        public FabricaDBContext(DbContextOptions<FabricaDBContext> options) : base(options)
        {

        }
    }
}
