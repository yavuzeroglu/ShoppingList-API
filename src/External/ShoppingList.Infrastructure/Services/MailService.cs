using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using ShoppingList.Application.Abstractions.Services;

namespace ShoppingList.Infrastructure.Services;

public class MailService : IMailService
{
   private readonly IConfiguration _configuration;

   public MailService(IConfiguration configuration)
   {
      _configuration = configuration;
   }

   public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
   {
      await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
   }

   public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
   {
      MailMessage mail = new();
      mail.IsBodyHtml = isBodyHtml;
      foreach (var to in tos)
         mail.To.Add(to);
      mail.Subject = subject;
      mail.Body = body;
      mail.From = new("yavuzeroglu15@gmail.com", "Yavuz Eroğlu", System.Text.Encoding.UTF8);


      SmtpClient smtp = new();
      smtp.Credentials = new NetworkCredential(
         _configuration["Mail:Username"],
         _configuration["Mail:Password"]
      );
      smtp.Port = Convert.ToInt32(_configuration["Mail:Port"]);
      smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
      smtp.Host = _configuration["Mail:Host"];
      smtp.EnableSsl = true;
      await smtp.SendMailAsync(mail);
   }
}