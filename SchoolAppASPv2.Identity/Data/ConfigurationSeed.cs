using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SchoolAppASPv2.Identity.Data
{
    public class ConfigurationSeed
    {
        public async Task SeedAsync(ApplicationDbContext context, IWebHostEnvironment env, ILogger<ApplicationDbContextSeed> logger, IServiceScope scope)
        {
            try
            {
                await RegisterApplicationsAsync(scope.ServiceProvider);
                await RegisterScopesAsync(scope.ServiceProvider);


                static async Task RegisterApplicationsAsync(IServiceProvider provider)
                {
                    var authSetting = provider.GetRequiredService<IOptions<AuthSettingModel>>();

                    var manager = provider.GetRequiredService<IOpenIddictApplicationManager>();
                    if (await manager.FindByClientIdAsync("ninja-client") == null)
                    {
                        await manager.CreateAsync(new OpenIddictApplicationDescriptor
                        {
                            ClientId = "ninja-client",
                            ClientSecret = "C4BBED05-A7C1-4759-99B5-0F84A29F0E08",
                            DisplayName = "Ninja Client Application",
                            Permissions =
                            {
                                Permissions.Endpoints.Token,
                                Permissions.GrantTypes.ClientCredentials
                            }
                        });
                    }
                    if (await manager.FindByClientIdAsync("postman-client") == null)
                    {
                        await manager.CreateAsync(new OpenIddictApplicationDescriptor
                        {
                            ClientId = "postman-client",
                            ClientSecret = "postman-secret", // Guid.NewGuid().ToString(),
                            ConsentType = ConsentTypes.Explicit,
                            DisplayName = "Postman Client Application",
                            RedirectUris = { new Uri(authSetting.Value.PostManUrl) },
                            Permissions =
                            {
                                Permissions.Endpoints.Authorization,
                                Permissions.Endpoints.Logout,
                                Permissions.Endpoints.Token,
                                Permissions.GrantTypes.AuthorizationCode,
                                Permissions.GrantTypes.RefreshToken,
                                Permissions.ResponseTypes.Code,
                                Permissions.Scopes.Email,
                                Permissions.Scopes.Profile,
                                Permissions.Scopes.Roles,
                                Permissions.Prefixes.Scope + "signal.system.web"
                            },
                            Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                        }); ;
                    }

                    if (await manager.FindByClientIdAsync("signal-frontend") == null)
                    {
                        await manager.CreateAsync(new OpenIddictApplicationDescriptor
                        {
                            ClientId = "signal-frontend",
                            ClientSecret = "signal-frontend-secret", // Guid.NewGuid().ToString(),
                            ConsentType = ConsentTypes.Explicit,
                            DisplayName = "Signal Systen Application",
                            PostLogoutRedirectUris =
                            {
                                new Uri($"{authSetting.Value.WebClientUrl}/signout-callback-oidc")
                            },
                            RedirectUris =
                            {
                                new Uri($"{authSetting.Value.WebClientUrl}/signin-oidc")
                            },
                            Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "signal.system.web"
                    },
                            Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                        }); ;
                    }
                    if (await manager.FindByClientIdAsync("angularClient") == null)
                    {
                        var descriptor = new OpenIddictApplicationDescriptor
                        {
                            ClientId = "angularClient",
                            DisplayName = "Anuglar client application",
                            PostLogoutRedirectUris =
                            {
                                new Uri("http://localhost:4200")
                            },
                            RedirectUris =
                            {
                                new Uri("http://localhost:4200")
                            },

                            ConsentType = ConsentTypes.Implicit,

                            Permissions =
                            {
                                Permissions.Endpoints.Authorization,
                                Permissions.Endpoints.Logout,
                                Permissions.GrantTypes.Implicit,
                                Permissions.ResponseTypes.IdToken,
                                Permissions.ResponseTypes.IdTokenToken,
                                Permissions.ResponseTypes.Token,
                                Permissions.Scopes.Email,
                                Permissions.Scopes.Profile,
                                Permissions.Prefixes.Scope + "signal.system.web",
                            }
                        };

                        await manager.CreateAsync(descriptor);
                    }
                    if (await manager.FindByClientIdAsync("signal_system_web_resource") == null)
                    {
                        var descriptor = new OpenIddictApplicationDescriptor
                        {
                            ClientId = "signal_system_web_resource",
                            ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                            Permissions =
                            {
                                Permissions.Endpoints.Introspection
                            }
                        };

                        await manager.CreateAsync(descriptor);
                    }

                }
                static async Task RegisterScopesAsync(IServiceProvider provider)
                {
                    var manager = provider.GetRequiredService<IOpenIddictScopeManager>();

                    if (await manager.FindByNameAsync("signal.system.web") is null)
                    {
                        var descriptor = new OpenIddictScopeDescriptor
                        {
                            Name = "signal.system.web",
                            Resources =
                            {
                                "signal_system_web_resource"
                            }
                        };

                        await manager.CreateAsync(descriptor);

                    }

                }



            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(ApplicationDbContext));
            }
        }
    }
}
