using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using TravelShare.EmailSending.Options;

namespace TravelShare.Services;

public class MailgunEmailSendService : IEmailSender
{
    private readonly MailgunOptions _mailgunOptions;
    public MailgunEmailSendService(IOptions<MailgunOptions> mailgunOptions)
    {
        _mailgunOptions = mailgunOptions.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(_mailgunOptions.ApiKey))
        {
            throw new Exception("No Mailgun API Key");
        }
        await ExecuteMailgunSend(toEmail, subject, message);
    }

    private async Task ExecuteMailgunSend(string toEmail, string subject, string message)
    {
        var baseUrl = new Uri("https://api.mailgun.net/v3");
        var mailgunOptions = new RestClientOptions(baseUrl)
        {
            Authenticator = new HttpBasicAuthenticator("api", _mailgunOptions.ApiKey)
        };
        var mailgunClient = new RestClient(mailgunOptions);
        
        RestRequest request = new RestRequest()
        {
            Resource = $"{_mailgunOptions.Domain}/messages"
        };
        request.AddParameter("domain", _mailgunOptions.Domain, ParameterType.UrlSegment);
        request.AddParameter("from", $"mailgun@{_mailgunOptions.Domain}");
        request.AddParameter("to", toEmail);
        request.AddParameter("subject", subject);
        request.AddParameter("html", message);
        request.Method = Method.Post;
        
        var response = await mailgunClient.PostAsync(request);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Email request sent to Mailgun has failed.");
        }
    }
}