using BikeSparePartsShop.Data;
using BikeSparePartsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIIntegrationTest
{
    public static class AddTestData
    {
        public static void AddStockData(ApplicationDbContext context)
        {
            var stockJsonString = File.ReadAllText("TestData/testStockData.json");

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var stockList = JsonSerializer.Deserialize<Stock[]>(stockJsonString, jsonOptions);
            {
                foreach(var stock in stockList)
                {
                    context.Add<Stock>(stock);
                }
                context.SaveChangesAsync();
            }
        }
    }
}
