using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class CheckTrustedGeoLocationTrigger
    {
        [FunctionName("CheckTrustedGeoLocationTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string id = req.Query["Id"];
            string latitude = req.Query["Latitude"];
            string longitude = req.Query["Longitude"];
            string responseMessage = "";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            id = id ?? data?.id;
            latitude = latitude ?? data?.latitude;
            longitude = longitude ?? data?.longitude;

            try{
                string connectionString = Environment.GetEnvironmentVariable("CCProjectDb");
                var db = new DatabaseContext(connectionString,id,latitude,longitude);
                responseMessage = db.CheckTrustedGeoLocation().ToString();
            }catch(Exception e){
                responseMessage = $"{e}";
            }

            

            return new OkObjectResult(responseMessage);
        }
    }
}
