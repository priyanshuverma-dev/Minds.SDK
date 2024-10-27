using Xunit;
using Minds.SDK;
namespace Minds.SDK.Tests
{


    public class ClientTests
    {

        private const string TestApiKey = "test-api-key";
        private const string TestBaseUrl = "https://dev.mindsdb.com";

        [Fact]
        public void Client_Should_Initialize_RestAPI_With_ApiKey_And_BaseUrl()
        {
            // Act
            var client = new Client(TestApiKey, TestBaseUrl);

            // Assert
            Assert.NotNull(client.Api);
            Assert.Equal(TestApiKey, client.Api.ApiKey);
            Assert.Equal(TestBaseUrl + "/api", client.Api.BaseUrl);
        }

        [Fact]
        public void Client_Should_Initialize_Datasources_And_Minds() {
            // Act
            var client = new Client(TestApiKey, TestBaseUrl);

            // Assert
            Assert.NotNull(client.Datasources);
            Assert.NotNull(client.Minds);
        }

        [Fact]
        public void Client_Should_Pass_Self_To_Datasources_And_Minds() {
            // Act
            var client = new Client(TestApiKey, TestBaseUrl);

            // Assert
            Assert.Equal(client, client.Datasources.Client);
            Assert.Equal(client, client.Minds.Client);
        }

        [Fact]
        public void Client_Should_Set_Default_BaseUrl_When_Not_Provided() {
            // Act
            var client = new Client(TestApiKey);

            // Assert
            Assert.NotNull(client.Api);
            Assert.Equal(TestApiKey, client.Api.ApiKey);
            Assert.Equal("https://mdb.ai/api", client.Api.BaseUrl);  // Assuming null baseUrl defaults to some URL in actual implementation
        }
    }
}