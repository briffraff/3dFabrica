namespace Fabrica.Tests
{
    using Fabrica.Data;
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Fabrica.Services;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class PropsServiceTests
    {
        public List<Prop> SeededPropsShouldBeVisualizedAtIndex()
        {
            var props = new List<Prop>()
            {
                new Prop()
                {
                    Name = "LS Crewneck",
                    Description = "Long Sleeve Pullover. 90% Cotton, 10% Silk",
                    PropType = PropType.LS,
                    Hashtags = "#LS #Pullover #Cotton",
                    ImageUrl = GlobalConstants.LSCrewneck,
                    Price = 56.5,
                },
                new Prop()
                {
                    Name = "SS Crewneck",
                    Description = "Short Sleeve Shirt. 50% Cotton, 50% Silk",
                    PropType = PropType.SS,
                    Hashtags = "#SS #T-shirt #50/50",
                    ImageUrl = GlobalConstants.SSCrewneck,
                    Price = 60,
                }
            };

            return props;
        }

        [Fact]
        public async Task GetProps_writeThemToDatabaseAndShowThoseWithPriceMoreThan50_ShouldReturnCount2()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var propsService = new PropsService(context);

            var props = new List<Prop>()
            {
                new Prop()
                {
                    Name = "LS Crewneck",
                    Description = "Long Sleeve Pullover. 90% Cotton, 10% Silk",
                    PropType = PropType.LS,
                    Hashtags = "#LS #Pullover #Cotton",
                    ImageUrl = GlobalConstants.LSCrewneck,
                    Price = 56.5,
                },
                new Prop()
                {
                    Name = "SS Crewneck",
                    Description = "Short Sleeve Shirt. 50% Cotton, 50% Silk",
                    PropType = PropType.SS,
                    Hashtags = "#SS #T-shirt #50/50",
                    ImageUrl = GlobalConstants.SSCrewneck,
                    Price = 60,
                }
            };

            context.Props.AddRange(props);
            await context.SaveChangesAsync();
            
            var count = await context.Props.Where(x=>x.Price > 50).CountAsync();
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetPropsWhichTypeIsShortSleeve_shouldReturnTrue()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var propsService = new PropsService(context);

            var props = SeededPropsShouldBeVisualizedAtIndex();
            await context.Props.AddRangeAsync(props);
            await context.SaveChangesAsync();

            var expected = 1;
            var actualCount = await context.Props.Where(x => x.PropType == PropType.SS).CountAsync();
            Assert.True(actualCount == expected);
        }

    }
}
