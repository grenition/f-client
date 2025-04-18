using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace f_client.Core.Handlers;

public class AuthMessageHandler : DelegatingHandler
{
    private readonly IJSRuntime _jsRuntime;

    public AuthMessageHandler(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
