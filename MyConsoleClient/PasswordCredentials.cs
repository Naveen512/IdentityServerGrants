using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleClient
{
    class PasswordCredentials
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

            var tokentClient = new TokenClient(discoveryDocument.TokenEndpoint, "password.Client", "secret");
            var tokenResponse = await tokentClient.RequestResourceOwnerPasswordAsync("alice","password");

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
