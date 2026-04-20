
using HamsterHub.Admin.Menus;
using HamsterHub.Admin.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5109");
    client.Timeout = TimeSpan.FromSeconds(30);
});

var provider = services.BuildServiceProvider();
var apiService = provider.GetRequiredService<ApiService>();

var mainMenu = new MainMenu(apiService);
await mainMenu.ShowAsync();

Console.WriteLine("\n Stänger ner HamsterHub...");

