using System.Net.Http.Headers;

namespace Performance;

public class RestClient
{
    private static readonly HttpClient _client = new();

    public async Task CallIntercepted() => await CallRestService("posts", 5001);

    public async Task CallNormal() => await CallRestService("tests", 5002);

    private async Task CallRestService(string controller, int port)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var callRest = await _client.GetAsync($"https://localhost:{port}/api/{controller}");
        await callRest.Content.ReadAsStringAsync();
    }
}