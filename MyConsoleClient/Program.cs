using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsyn().GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static async Task MainAsyn()
        {
            var discoveryDocument = await DiscoveryClient.GetAsync("http://localhost:5000");

            if (discoveryDocument.IsError)
            {
                Console.WriteLine(discoveryDocument.Error);
                return;
            }

            var tokentClient = new TokenClient(discoveryDocument.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokentClient.RequestClientCredentialsAsync();

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

        }
    }
}
