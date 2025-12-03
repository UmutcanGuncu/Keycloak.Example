using Keycloak.Example.Services;
using Keycloak.Example.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.Configure<KeycloakConfiguration>(builder.Configuration.GetSection("KeycloakConfiguration"));
builder.Services.AddScoped<KeycloakService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/get-access-token", async (KeycloakService keycloakService) =>
{
    var token = await keycloakService.GetAccessTokenAsync(default);
    return Results.Ok(new {AccessToken = token});
});
app.Run();