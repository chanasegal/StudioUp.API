using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using MimeKit;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class EmailService
{
    private static readonly string[] Scopes = { GmailService.Scope.GmailSend };
    private static readonly string ApplicationName = "StudioUp";
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public async Task SendEmailAsync(string toAddress, string subject, string body)
    {
        UserCredential credential;
        var credentialsFilePath = _configuration["EmailSettings:CredentialsFilePath"];


        using (var stream = new FileStream(credentialsFilePath, FileMode.Open, FileAccess.Read))
        {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None);
        }

        var service = new GmailService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        var message = new MimeMessage();
        var senderName = _configuration["EmailSettings:SenderName"];
        var senderEmail = _configuration["EmailSettings:SenderEmail"];
        message.From.Add(new MailboxAddress(senderName, senderEmail));
        message.To.Add(new MailboxAddress("", toAddress));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using (var stream = new MemoryStream())
        {
            await message.WriteToAsync(stream);
            var gmailMessage = new Message
            {
                Raw = Convert.ToBase64String(stream.ToArray())
            };

            await service.Users.Messages.Send(gmailMessage, "me").ExecuteAsync();
        }
    }
}
