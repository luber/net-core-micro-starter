using System;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Auth.Service
{
    public static class InMemoryConfiguration
    {
        /// <summary>
        /// The resources that you want to protect.
        /// The client will use the scope parameter to request access to an identity resource.
        /// </summary>
        /// <returns>The identity resources.</returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            //var customProfile = new IdentityResource(
                //name: "custom.profile",
                //displayName: "Custom profile",
                //claimTypes: new[] { "name", "email", "status" });

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                //customProfile
            };
        }

        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// </summary>
        /// <returns>The API resources.</returns>
        public static IEnumerable<ApiResource> GetApiResources() {
            return new ApiResource[]
            {
                new ApiResource("api-gw", "Api Gateway"),
                new ApiResource("svc1", "Service 1"), // TODO: check if we need this? in case we use self-contained JWTs
                new ApiResource("svc2", "Service 2"),
                //new ApiResource
                //{
                //    Name = "api2",

                //    // secret for using introspection endpoint
                //    ApiSecrets =
                //    {
                //        new Secret("secret".Sha256())
                //    },

                //    // include the following using claims in access token (in addition to subject id)
                //    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email },

                //    // this API defines two scopes
                //    Scopes =
                //    {
                //        new Scope()
                //        {
                //            Name = "api2.full_access",
                //            DisplayName = "Full access to API 2",
                //        },
                //        new Scope
                //        {
                //            Name = "api2.read_only",
                //            DisplayName = "Read only access to API 2"
                //        }
                //    }
                //}
            };
        }

        /// <summary>
        /// Clients represent applications that can request tokens from your identityserver.
        /// </summary>
        /// <returns>The clients.</returns>
        public static IEnumerable<Client> GetClients() {
            return new Client[]
            {
                new Client {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    // a way to specify how a client wants to interact with IdentityServer.
                    //AllowedGrantTypes = GrantTypes.ClientCredentials, // used to server-server communication. needed ClientId and secret
                    //AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // This is so called “non-interactive” flow. not recommended. allows to request tokens on behalf of a user. needed userName & password.
                    AllowedGrantTypes = GrantTypes.Implicit, // optimized for browser-based applications. all tokens are transmitted via the browser, and advanced features like refresh tokens are thus not allowed.
                    AllowAccessTokensViaBrowser = true,
                    //AllowedGrantTypes = GrantTypes.Hybrid, // a combination of the implicit and authorization code flow. is used for server-side web applications and native desktop/mobile applications.
                    //AllowOfflineAccess = true, // required for refresh_tokens
                    AccessTokenType = AccessTokenType.Jwt, // self-contained JWT
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,

                        //"api1", "api2.read_only"
                        "api-gw", "svc1", "svc2"
                    }
                }
            };
        }
    }
}
