using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ControlSSH.Client;
using ControlSSH.Client.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("default", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddMudServices();

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<HttpService>();

await builder.Build().RunAsync();