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

    public class MarvelousPropsServiceTests
    {
        public List<MarvelousProp> SeededMarvsShouldBeVisualizedAtIndex()
        {
            var marvelousProps = new List<MarvelousProp>()
            {
                new MarvelousProp()
                {
                    Name = "Space collection - Mars",
                    Description = "Collection of space suits models from Mars Expedition. Parachutes and landing balls.",
                    PropType = MarvelousType.Astronauts,
                    Hashtags = "#Mars #expedition #Suits #parachutes #balls",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 1200,
                },
                new MarvelousProp()
                {
                    Name = "Park and chill",
                    Description = "Holiday equipment for lazy days. Ropes,hammocks,inflatable pillows.",
                    PropType = MarvelousType.Holiday,
                    Hashtags = "#holiday #lazy #hammocks",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 1000,
                },
            };

            return marvelousProps;
        }

        [Fact]
        public async Task GetMarvs_writeThemToDatabaseAndShowThoseWithPriceMoreThan50_ShouldReturnCount2()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var marvelousPropsService = new MarvelousPropsService(context);

            var marvelousProps = new List<MarvelousProp>()
            {
                new MarvelousProp()
                {
                    Name = "Space collection - Mars",
                    Description = "Collection of space suits models from Mars Expedition. Parachutes and landing balls.",
                    PropType = MarvelousType.Astronauts,
                    Hashtags = "#Mars #expedition #Suits #parachutes #balls",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 1200,
                },
                new MarvelousProp()
                {
                    Name = "Park and chill",
                    Description = "Holiday equipment for lazy days. Ropes,hammocks,inflatable pillows.",
                    PropType = MarvelousType.Holiday,
                    Hashtags = "#holiday #lazy #hammocks",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 1000,
                },
            };

            context.MarvelousProps.AddRange(marvelousProps);
            await context.SaveChangesAsync();

            var count = await context.MarvelousProps.Where(x => x.Points > 500).CountAsync();
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetPropsWhichTypeIsShortSleeve_shouldReturnTrue()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var marvsService = new MarvelousPropsService(context);

            var marvs = SeededMarvsShouldBeVisualizedAtIndex();
            await context.MarvelousProps.AddRangeAsync(marvs);
            await context.SaveChangesAsync();

            var expected = 1;
            var actualCount = await context.MarvelousProps.Where(x => x.PropType == MarvelousType.Holiday).CountAsync();
            Assert.True(actualCount == expected);
        }
    }
}
