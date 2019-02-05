using Microsoft.Extensions.DependencyInjection;
using Realmdigital_Interview.Controllers;
using Realmdigital_Interview.Services;
using System;
using System.Net;
using Xunit;

namespace Controller.Tests
{    
    public class ProductController_Get
    {
        public void GetReturnSimpleString()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IWebClient, Realmdigital_Interview.Services.WebClient>();

            var controller = new ProductController(serviceCollection);


            var response = controller.Get();

            return response.Result;
        }


        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "welcome" };
        }

    }
}
