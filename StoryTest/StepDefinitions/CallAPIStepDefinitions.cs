using System.Net.Http.Headers;
using System.Net;
using TechTalk.SpecFlow;
using Microsoft.AspNetCore.Mvc.Testing;
using API;
using Service;
using Common.DTOs.Users;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P6.StoryTest.StepDefinitions
{
    [Binding]
    public class CallAPIStepDefinitions
    {
        private readonly ScenarioContext context;
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private string projectDir;
        private string configPath;
        private HttpClient client;
        IConfigurationRoot config;

        public CallAPIStepDefinitions(
          ScenarioContext context,
          WebApplicationFactory<Startup> webApplicationFactory)
        {
            // Inject auto test connection string to application under test
            projectDir = Directory.GetCurrentDirectory();
            configPath = Path.Combine(projectDir, "appsettings.test.json");
            config = new ConfigurationBuilder().AddJsonFile(configPath).Build();

            this.context = context;
            this.webApplicationFactory = webApplicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });
            });

            // create an http client of the application under test
            client = this.webApplicationFactory.CreateClient();

            // prepare an empty database for auto test
            using var provider = new ServiceCollection()
                //.AddDbContext<Data.EF.EFContext>(options =>
                //     options.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=DDDSample1;Trusted_Connection=True;MultipleActiveResultSets=true"))
                .AddDbContext<Data.EF.EFContext>(options =>
                     options.UseSqlServer(config.GetConnectionString("DDDConnectionString")))
                .AddScoped<Data.EF.EFContext>()
                .BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Data.EF.EFContext>();
                db.Database.ExecuteSqlRaw("Drop Table IF Exists Payslips");
                db.Database.ExecuteSqlRaw("Drop Table IF Exists Users");
                db.Database.ExecuteSqlRaw("Drop Table IF Exists Departments");
                db.Database.ExecuteSqlRaw("Drop Table IF Exists __EFMigrationsHistory");
                db.Database.Migrate();
            }

        }
        [Given(@"I have the following request body:")]
        public void GivenIHaveTheFollowingRequestBody(string multilineText)
        {
            context.Set(multilineText, "Request");
        }

        [When(@"I post this request to the ""([^""]*)"" operation")]
        public async Task WhenIPostThisRequestToTheOperation(string users)
        {
            var requestBody = context.Get<string>("Request");
            // set up Http Request Message
            var request = new HttpRequestMessage(HttpMethod.Post, $"/{users}")
            {
                Content = new StringContent(requestBody)
                {
                    Headers =
                    {
                      ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            // let's post
            var response = await client.SendAsync(request).ConfigureAwait(false);
            try
            {
                context.Set(response.StatusCode, "ResponseStatusCode");
                context.Set(response.ReasonPhrase, "ResponseReasonPhrase");
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                context.Set(responseBody, "ResponseBody");
            }
            finally
            {
                // move along, move along
            }
        }

        [Then(@"the result is a (.*) \(""([^""]*)""\) response")]
        public void ThenTheResultIsAResponse(int statusCode, string ResponseStatusCode)
        {
            Assert.AreEqual(statusCode, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));
            Assert.AreEqual(ResponseStatusCode, context.Get<string>("ResponseReasonPhrase"));
        }

        [Then(@"the response body description username is \(""([^""]*)""\) and ID is \((.*)\)")]
        public void ThenTheResponseBodyDescriptionUsernameIsAndIDIs(string micl, int p1)
        {
            AddUserResponse result = JsonConvert.DeserializeObject<AddUserResponse>(context.Get<string>("ResponseBody"));
            Assert.AreEqual(result.UserName, micl);
            Assert.AreEqual(result.Id, p1);
        }


    }
}
