using Microsoft.Graph;
using Microsoft.AspNetCore.Diagnostics;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Azure.Identity;

namespace ApplicationServices.Graph;

public class GraphClient
{

    private readonly IConfiguration _configuration;
    public GraphClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public GraphServiceClient GetClient()
    {
        var scopes = new[] { "https://graph.microsoft.com/.default" };
        var tenantId = _configuration["Graph:TenantId"];
        var clientId = _configuration["Graph:ClientId"];
        var clientSecret = _configuration["Graph:ClientSecret"];

        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };

        var clientSecretCredential = new ClientSecretCredential(
            tenantId, clientId, clientSecret, options);

        var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
        return graphClient;
    }

}