using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realmdigital_Interview.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Realmdigital_Interview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IServiceProvider serviceProvider;

        public ProductController(IServiceProvider serviceProvider)
        {
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"welcome"};
        }
        
        [HttpGet("{id}")]
        public object GetProductById(string id)
        {
            string response = "";

            var scope = serviceProvider.CreateScope();
            using (var client = scope.ServiceProvider.GetRequiredService<IWebClient>())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = client.UploadString("http://192.168.0.241/eanlist?type=Web", "POST", "{ \"id\": \"" + id + "\" }");
            }
            
            var reponseObject = JsonConvert.DeserializeObject<List<ApiResponseProduct>>(response);

            var result = new List<object>();
            for (int i = 0; i < reponseObject.Count; i++)
            {
                var prices = new List<object>();
                for (int j = 0; j < reponseObject[i].PriceRecords.Count; j++)
                {
                    if (reponseObject[i].PriceRecords[j].CurrencyCode == "ZAR")
                    {
                        prices.Add(new
                        {
                            Price = reponseObject[i].PriceRecords[j].SellingPrice,
                            Currency = reponseObject[i].PriceRecords[j].CurrencyCode
                        });
                    }
                }
                result.Add(new
                {
                    Id = reponseObject[i].BarCode,
                    Name = reponseObject[i].ItemName,
                    Prices = prices
                });
            }
            return result.Count > 0 ? result[0] : null;
        }

        [HttpGet("search/{productName}")]
        public List<object> GetProductsByName(string productName)
        {
            string response = "";

            var scope = serviceProvider.CreateScope();
            using (var client = scope.ServiceProvider.GetRequiredService<IWebClient>())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = client.UploadString("http://192.168.0.241/eanlist?type=Web", "POST", "{ \"names\": \"" + productName + "\" }");
            }
         
            var reponseObject = JsonConvert.DeserializeObject<List<ApiResponseProduct>>(response);

            var result = new List<object>();
            for (int i = 0; i < reponseObject.Count; i++)
            {
                var prices = new List<object>();
                for (int j = 0; j < reponseObject[i].PriceRecords.Count; j++)
                {
                    if (reponseObject[i].PriceRecords[j].CurrencyCode == "ZAR")
                    {
                        prices.Add(new
                        {
                            Price = reponseObject[i].PriceRecords[j].SellingPrice,
                            Currency = reponseObject[i].PriceRecords[j].CurrencyCode
                        });
                    }
                }
                result.Add(new
                {
                    Id = reponseObject[i].BarCode,
                    Name = reponseObject[i].ItemName,
                    Prices = prices
                });
            }
            return result;
        }
        
        class ApiResponseProduct
        {
            public string BarCode { get; set; }
            public string ItemName { get; set; }
            public List<ApiResponsePrice> PriceRecords { get; set; }
        }

        class ApiResponsePrice
        {
            public string SellingPrice { get; set; }
            public string CurrencyCode { get; set; }
        }
    }
}