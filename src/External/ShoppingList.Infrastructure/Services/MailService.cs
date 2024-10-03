using System.Net;
using System.Net.Mail;
using System.Text;
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

   public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
   {
      await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
   }

   public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
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

   public async Task SendPasswordMailAsync(string to, string userId, string resetToken)
   {
      StringBuilder mail = new();
      mail.AppendLine("Merhaba <br> <br> Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten yeni şifre belirleyebilirsiniz. ");
      mail.Append("<a target=\"_blank\" href=\"");
      mail.Append(_configuration["BaseSampleUrl"]);
      mail.Append("/password-reset/");
      mail.Append(userId);
      mail.Append("/");
      mail.Append(resetToken);
      mail.Append("\">Yeni Şifre talebi için tıklayınz.</a> <br> <br> <br> <span> YE - Always Learner </span>");
      await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
   }
}