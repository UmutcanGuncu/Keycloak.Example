using System.Text.Json;
using Keycloak.Example.Dtos;
using Keycloak.Example.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace Keycloak.Example.Services;

public class KeycloakService(IOptions<KeycloakConfiguration> options)
{
    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        string endpoint = $"{options.Value.HostName}/realms/{options.Value.Realm}/protocol/openid-connect/token";
        List<KeyValuePair<string, string>> datas = new();
        KeyValuePair<string,string> grandType = new KeyValuePair<string, string>("grant_type","client_credentials");
        KeyValuePair<string,string> clientId = new KeyValuePair<string, string>("client_id",options.Value.ClientId);
        KeyValuePair<string,string> clientSecret = new KeyValuePair<string, string>("client_secret",options.Value.ClientSecret);
        datas.Add(grandType);
        datas.Add(clientId);
        datas.Add(clientSecret);
        var message = await httpClient.PostAsync(endpoint, new FormUrlEncodedContent(datas), cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
           
            if (message.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
             var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);
             throw new ArgumentException(errorResultForBadRequest.ErrorMessage);
            }
            var errorResultForOther = JsonSerializer.Deserialize<ErrorResponseDto>(response);

            throw new ArgumentException(errorResultForOther.ErrorDescription);
        }
        var result = JsonSerializer.Deserialize<GetAccessTokenResponseDto>(response);
        return result.AccessToken;
    }
}