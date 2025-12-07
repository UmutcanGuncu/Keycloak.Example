using Keycloak.Example.Dtos;
using Keycloak.Example.Services;
using Keycloak.Example.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Keycloak.Example.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[AllowAnonymous]
public class AuthController(KeycloakService keycloakService,IOptions<KeycloakConfiguration>options) : ControllerBase
{
   [HttpPost]
   public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
   {
      await keycloakService.RegisterAsync(registerUserDto);
      return Ok();
   }

   [HttpPost]
   public async Task<IActionResult> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken)
   {
      /*
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
      await keycloakService.PostUrlEncodedFormPostAsync(endpoint, datas,false, cancellationToken);
      */
      var result = await keycloakService.LoginAsync(loginUserDto, cancellationToken);
      return Ok(result);
   }
}