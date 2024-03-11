using AylinChat.Authentication;
using AylinChat.Client.AppState;
using AylinChat.Client.ChatServices;
using AylinChat.Components;
using AylinChat.Data;
using AylinChat.Hubs;
using AylinChat.Repos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<MyHubConnectionService>();

builder.Services.AddDbContext<AppDbContext>
    (o => o.UseSqlite(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddControllers();
builder.Services.AddScoped<ChatRepo>();
builder.Services.AddSignalR();
builder.Services.AddScoped<AvailableUserState>();

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = IdentityConstants.ApplicationScheme;
    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider,PSASP>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AylinChat.Client._Imports).Assembly);

app.MapHub<ChatHub>("/Hubs");
app.MapControllers();
app.MapAdditionalIdentityEnpoints();

app.Run();
