using System.Net.Http.Headers;
using System.Net;
using TechTalk.SpecFlow;
using Microsoft.AspNetCore.Mvc.Testing;
using API;
using Newtonsoft.Json;

namespace P6.StoryTest.StepDefinitions
{
    public class AddUserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    } 
    [Binding]
    public class CallAPIStepDefinitions
    {
        private readonly ScenarioContext context;
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        public CallAPIStepDefinitions(
          ScenarioContext context,
          WebApplicationFactory<Startup> webApplicationFactory)
        {
            this.context = context;
            this.webApplicationFactory = webApplicationFactory;
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
            // create an http client
            var client = webApplicationFactory.CreateClient();
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

        [Then(@"the response body description username is \(""([^""]*)""\)")]
        public void ThenTheResponseBodyDescriptionUsernameIs(string micl)
        {
            AddUserResponse result = JsonConvert.DeserializeObject<AddUserResponse>(context.Get<string>("ResponseBody"));
            Assert.AreEqual(result.UserName, micl);
        }

    }
}
