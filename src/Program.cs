using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using f_client;
using f_client.Core.Handlers;
using f_client.Core.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["ApiUrl"];
var baseAddress = !string.IsNullOrEmpty(apiUrl)
    ? new Uri(apiUrl)
    : new Uri(builder.HostEnvironment.BaseAddress);

builder.Services.AddScoped(sp =>
{
    var jsRuntime = sp.GetRequiredService<IJSRuntime>();
    var client = new HttpClient(new AuthMessageHandler(jsRuntime))
    {
        BaseAddress = baseAddress
    };
    return client;
});


builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
