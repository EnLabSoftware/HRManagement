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
    public class CallAPIStepDefinitions : StepDefinitionBase
    {
        public CallAPIStepDefinitions(
          ScenarioContext context,
          WebApplicationFactory<Startup> webApplicationFactory) : base(context, webApplicationFactory)
        {
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
