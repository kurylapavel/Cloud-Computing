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
    public static class CCProjectTrigger
    {
        [FunctionName("CCProjectTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string login = req.Query["log"];
            string password = req.Query["pass"];

            string responseMessage = "";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            login = login ?? data?.login;
            password = password ?? data?.password;

            try{
                string connectionString = Environment.GetEnvironmentVariable("CCProjectDb");
                var db = new DatabaseContext(connectionString,login,password);
                responseMessage = db.GetUserId().ToString();
            }catch(Exception e){
                responseMessage = $"{e}";
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
