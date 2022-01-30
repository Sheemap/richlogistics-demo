using Microsoft.AspNetCore.Mvc.Routing;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HexDemoSite.Services;

public class SendGridService
{
    private readonly SendGridClient _client;
    private readonly string _baseUrl;
    private readonly string _leadershipEmail;
    
    public SendGridService(IConfiguration config)
    {
        _client = new SendGridClient(config.GetValue<string>("SENDGRID_APIKEY"));
        _baseUrl = config.GetValue<string>("BaseUrl");
        _leadershipEmail = config.GetValue<string>("LeadershipEmail");
    }

    public async Task SendHireRequestApprovalAsync(int positionId, string approvalCode)
    {
        var url = $"{_baseUrl}/home/leadershipapproval/{positionId}?code={approvalCode}";
        var from = new EmailAddress("no-reply@snazcat.com");
        var to = new EmailAddress(_leadershipEmail);
        var templatedEmail = MailHelper.CreateSingleTemplateEmail(from, to, "d-2eb7adfeac8646f1960a6728a90f23f3", new { url });
        await _client.SendEmailAsync(templatedEmail);
    }

    public async Task SendHiredEmailAsync(int candidateId, string candidateEmail, string roleName)
    {
        var url = $"{_baseUrl}/home/candidateconfirm/{candidateId}";
        var from = new EmailAddress("no-reply@snazcat.com");
        var to = new EmailAddress(candidateEmail);
        var templatedEmail = MailHelper.CreateSingleTemplateEmail(from, to, "d-0646ad6fb2a542379fccc00f5c30191c", new { roleName, url });
        await _client.SendEmailAsync(templatedEmail);
    }

    public async Task SendRejectedEmailsAsync(params string[] emails)
    {
        var from = new EmailAddress("no-reply@snazcat.com");
        var recipients = emails.Select(x => new EmailAddress(x)).ToList();
        
        var templatedEmails = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, recipients, "d-04c2d8e842d74447a5e0bbac52a3953e", new {});
        await _client.SendEmailAsync(templatedEmails);
    }
}