using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityServer
{
    public class Config
    {
        
        public static List<ApiResource> GetApiResource()
        {
            return new List<ApiResource> {
                new ApiResource(
                    "api1",
                     "Testing api"
                )

            };
        }

        public static List<IdentityResource> GetIdentityResource()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static List<Client> GetClient()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "console client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = { "api1" }

                },

                new Client
                {
                    ClientId = "password.Client",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {"api1"}


                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "mvc project",
                    AllowedGrantTypes =  new List<string>{ GrantType.AuthorizationCode },
                     ClientSecrets =
    {
        new Secret("secret".Sha256())
    },

                    RedirectUris = { "http://localhost:5002/Home/Index" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                 AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        "api1"
    },
                   AllowOfflineAccess = true

                },
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:5004/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5004/index.html" },
                 AllowedCorsOrigins =     { "http://localhost:5004" },
                 AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        "api1"
    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "naveen",
                    Password = "password"
                },
                
            };
        }
    }
}
