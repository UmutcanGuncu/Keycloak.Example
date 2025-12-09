using System.Text;
using System.Text.Json;
using Keycloak.Example.Dtos;
using Keycloak.Example.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        KeyValuePair<string, string> grandType = new KeyValuePair<string, string>("grant_type", "client_credentials");
        KeyValuePair<string, string> clientId = new KeyValuePair<string, string>("client_id", options.Value.ClientId);
        KeyValuePair<string, string> clientSecret =
            new KeyValuePair<string, string>("client_secret", options.Value.ClientSecret);
        datas.Add(grandType);
        datas.Add(clientId);
        datas.Add(clientSecret);
        var message = await httpClient.PostAsync(endpoint, new FormUrlEncodedContent(datas), cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
        {

            if (message.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<ErrorResponseDto>(response);
                throw new ArgumentException(errorResultForBadRequest.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

            throw new ArgumentException(errorResultForOther.ErrorMessage);
        }

        var result = JsonSerializer.Deserialize<GetAccessTokenResponseDto>(response);
        return result!.AccessToken;
    }

    public async Task RegisterAsync(RegisterUserDto registerUserDto)
    {
        
        object body = new
        {
            username = registerUserDto.Username,
            firstName = registerUserDto.FirstName,
            email = registerUserDto.Email,
            enabled = true,
            emailVerified = false,
            credentials = new List<object>
            {
                new
                {
                    type = "password",
                    temporary = false,
                    value = registerUserDto.Password
                }
            }
            
            

        };
        HttpClient  httpClient = new();
        
        string endpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users";
        var bodyForJson = JsonSerializer.Serialize(body);
        string token = await GetAccessTokenAsync();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}" );
        var content = new  StringContent(bodyForJson, Encoding.UTF8, "application/json"); 
        var message = await httpClient.PostAsync(endpoint,content);
        var response = await message.Content.ReadAsStringAsync();
        if (!message.IsSuccessStatusCode)
        {

            if (message.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<ErrorResponseDto>(response);
                throw new ArgumentException(errorResultForBadRequest.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

            throw new ArgumentException(errorResultForOther.ErrorMessage);
        }
        
    }

    public async Task PostAsync(string endpoint, object data, bool requiredToken = false ,CancellationToken cancellationToken = default)
    {
        HttpClient  httpClient = new();
        var bodyForJson = JsonSerializer.Serialize(data);
        if (requiredToken)
        {
            string token = await GetAccessTokenAsync(cancellationToken);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}" );
        }
        var content = new  StringContent(bodyForJson, Encoding.UTF8, "application/json"); 
        var message = await httpClient.PostAsync(endpoint,content,cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
        {

            if (message.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<ErrorResponseDto>(response);
                throw new ArgumentException(errorResultForBadRequest.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

            throw new ArgumentException(errorResultForOther.ErrorMessage);
        }
    }

    public async Task PostUrlEncodedFormPostAsync(string endpoint, List<KeyValuePair<string,string>> data, bool requiredToken = false,
        CancellationToken cancellationToken = default)
    {
        HttpClient  httpClient = new();
        if (requiredToken)
        {   
            string token = await GetAccessTokenAsync(cancellationToken);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        var message = await httpClient.PostAsync(endpoint,new FormUrlEncodedContent(data),cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            throw new ArgumentException(response.ToString());
        }
        var deneme = JsonSerializer.Serialize(response);
    }

    public async Task<LoginUserResultDto> LoginAsync(LoginUserDto loginUserDto,
        CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new HttpClient();
        string endpoint = $"{options.Value.HostName}/realms/{options.Value.Realm}/protocol/openid-connect/token";
        List<KeyValuePair<string, string>> datas = new();
        KeyValuePair<string, string> grandType = new KeyValuePair<string, string>("grant_type", "password");
        KeyValuePair<string, string> clientId = new KeyValuePair<string, string>("client_id", options.Value.ClientId);
        KeyValuePair<string, string> clientSecret = new KeyValuePair<string, string>("client_secret", options.Value.ClientSecret);
        KeyValuePair<string, string> username = new KeyValuePair<string, string>("username", loginUserDto.Username);
        KeyValuePair<string, string> password = new KeyValuePair<string, string>("password", loginUserDto.Password);
        datas.Add(grandType);
        datas.Add(clientId);
        datas.Add(clientSecret);
        datas.Add(username);
        datas.Add(password);
        var message = await httpClient.PostAsync(endpoint,new FormUrlEncodedContent(datas),cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            throw new ArgumentException();
        }
        var result = JsonSerializer.Deserialize<LoginUserResultDto>(response);
        return result;
    }
    public async Task<IEnumerable<GetAllUsersResultDto>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        HttpClient  httpClient = new();
        var endpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}" );
        var message = await httpClient.GetAsync(endpoint, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            throw new ArgumentException(response.ToString());
        }
        var result = JsonSerializer.Deserialize<IEnumerable<GetAllUsersResultDto>>(response);
        return result;
    }
    public async Task<GetUserResultDto> GetById(string id, CancellationToken cancellationToken = default)
    {
        HttpClient  httpClient = new();
        var endpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users/{id}";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var message = await httpClient.GetAsync(endpoint, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        var result = JsonSerializer.Deserialize<GetUserResultDto>(response);
        return result;
    }

    public async Task<List<GetUserResultDto>> GetByUsername(string username, CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var endpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users?username={username}";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization",$"Bearer {token}");
        var message = await httpClient.GetAsync(endpoint, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if(!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        var result = JsonSerializer.Deserialize<List<GetUserResultDto>>(response);
        return result;
    }

    public async Task UpdateUser(string id, UserDto userDto, CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var bodyForJson = JsonSerializer.Serialize(userDto);
        var url = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users/{id}";
        var content = new StringContent(bodyForJson, Encoding.UTF8, "application/json");
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var message = await httpClient.PutAsync(url, content, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        
    }

    public async Task DeleteUser(string id, CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var url = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users/{id}";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var message = await httpClient.DeleteAsync(url, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        
    }
    public async Task<IEnumerable<GetRolesResultDto>> GetAllRoles(CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var url =
            $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUuid}/roles";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var message = await httpClient.GetAsync(url, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        var result = JsonSerializer.Deserialize<IEnumerable<GetRolesResultDto>>(response);
        return result;
    }

    public async Task<GetRolesResultDto> GetRoleByName(string roleName, CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var url =
            $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUuid}/roles/{roleName}";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var message = await httpClient.GetAsync(url, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        var result = JsonSerializer.Deserialize<GetRolesResultDto>(response);
        return result;
    }

    public async Task CreateRole(CreateRoleDto createRoleDto, CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var bodyForJson = JsonSerializer.Serialize(createRoleDto);
        var url =
            $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUuid}/roles";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var stringContent = new StringContent(bodyForJson, Encoding.UTF8, "application/json");
        var message = await httpClient.PostAsync(url, stringContent, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        
    }
    public async Task DeleteRole(string roleName, CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var url =
            $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUuid}/roles/{roleName}";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var message = await httpClient.DeleteAsync(url, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
    }

    public async Task AssignRolesToUser(Guid userId, IEnumerable<RoleDto> roleDto,
        CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var url = 
            $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users/{userId}/role-mappings/clients/{options.Value.ClientUuid}";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var json = JsonSerializer.Serialize(roleDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var message = await httpClient.PostAsync(url, content, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        
    }

    public async Task RemoveRolesFromUser(Guid userId, IEnumerable<RoleDto> roleDto,
        CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var url = 
            $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users/{userId}/role-mappings/clients/{options.Value.ClientUuid}";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        var json = JsonSerializer.Serialize(roleDto);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        var message = await httpClient.SendAsync(request, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        
    }

    public async Task<IEnumerable<GetUserRolesDto>> GetUserRoles(Guid userId, CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = new();
        var url = 
            $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users/{userId}/role-mappings/clients/{options.Value.ClientUuid}";
        var token = await GetAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var message = await httpClient.GetAsync(url, cancellationToken);
        var response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (!message.IsSuccessStatusCode)
            throw new ArgumentException(response.ToString());
        var result = JsonSerializer.Deserialize<IEnumerable<GetUserRolesDto>>(response);
        return result;
    }
}