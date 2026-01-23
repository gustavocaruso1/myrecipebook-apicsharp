using Azure.Core;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public RegisterUserTest(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();


        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var response = await _httpClient.PostAsJsonAsync("User", request);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            await using var responseBody = await response.Content.ReadAsStreamAsync();  
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
        }
    }
}
