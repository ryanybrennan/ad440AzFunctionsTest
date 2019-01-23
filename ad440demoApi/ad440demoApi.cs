
using System;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace ad440demoApi
{
    public static class ad440demoApi
    {
        [FunctionName("ad440demoApi")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, 
        "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Well, {name}, have the lambs stopped screaming?")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
