using AylinChat.Client.ChatServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<ChatService>();
await builder.Build().RunAsync();
