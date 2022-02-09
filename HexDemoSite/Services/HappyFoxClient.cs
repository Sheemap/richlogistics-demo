using System.ComponentModel.Design;
using System.Text;
using RestSharp;

namespace HexDemoSite.Services;

public class HappyFoxClient
{
    private const string BaseUrl = "https://cat.happyfox.com";
    private const string Name = "Hex Demo";
    private const string Email = "hexdemo@richlogistics.com";
    private const int Category = 17;
    
    private readonly string _basicAuth;
    
    public HappyFoxClient(IConfiguration config)
    {
        var apiKey = config.GetValue<string>("HappyFoxApiKey");
        var authCode = config.GetValue<string>("HappyFoxAuthCode");
        _basicAuth = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{authCode}"))}";
    }

    public async Task CreateTicketAsync(string roleName, string hireName)
    {
        var client = new RestClient(BaseUrl);
        var request = new RestRequest("/api/1.1/json/tickets/", Method.Post);
        request.AddHeader("Authorization", _basicAuth);
        request.AddJsonBody(new
        {
            name = Name,
            email = Email,
            category = Category,
            subject = $"NEW HIRE - {roleName} - {hireName}",
            text = $"{hireName} has been hired for the position of {roleName}.",
        });

        var res = await client.ExecuteAsync(request);
    }
    
}