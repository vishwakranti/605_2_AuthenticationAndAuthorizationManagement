namespace APIIntegrationTest
{
    public class StockControllerIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public StockControllerIntegrationTest(TestingWebAppFactory<Program> factory) => 
                                                                                    _httpClient = factory.CreateClient();
        [Fact]
        public async void IndexReturnsStocks()
        {
            var response = await _httpClient.GetAsync("Stocks");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Lezyne Smart Patch Kit", responseString);
        }

        [Fact]
        public async void IndexApiReturnsStocks()
        {
            var response = await _httpClient.GetAsync("api/StocksApi");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Lezyne Smart Patch Kit", responseString);
        }
    }
}