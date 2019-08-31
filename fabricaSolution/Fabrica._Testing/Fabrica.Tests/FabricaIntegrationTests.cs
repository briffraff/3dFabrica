namespace Fabrica.Tests
{
    using Fabrica.Web;
    using Microsoft.AspNetCore.Mvc.Testing;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class FabricaIntegrationTests
    {
        private readonly WebApplicationFactory<Startup> server;
        private readonly HttpClient client;
        private readonly HttpResponseMessage requestUrl;

        public FabricaIntegrationTests()
        {
            this.server = new WebApplicationFactory<Startup>();
            this.client = server.CreateClient();
            this.requestUrl = client.GetAsync("/Home/Index").Result;
        }

        [Fact]
        public async Task VerifyIndexPageReturnsStatusOK()
        {
            var expected = true;
            var actual = requestUrl.IsSuccessStatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CheckIfTheTitleOfthePageExists()
        {
            var actualString = await requestUrl.Content.ReadAsStringAsync();
            var expected = "Index";

            Assert.DoesNotContain(expected, actualString);
        }

        [Fact]
        public async Task IndexPageShouldContainsTagStructure()
        {
            var actualString = await requestUrl.Content.ReadAsStringAsync();

            var expected = new List<string>
            {
                "<header","<nav","</nav>","</header>","<main","</main>","<footer","</footer>","<section","</section>",
            };

            foreach (var tag in expected)
            {
                Assert.Contains(tag, actualString);
            }
        }

    }
}
