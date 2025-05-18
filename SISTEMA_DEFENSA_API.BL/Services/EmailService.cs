using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SISTEMA_DEFENSA_API.EL.DbContexts;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var smtpConfig = _configuration.GetSection("SmtpSettings");

            using (var client = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"])))
            {
                client.Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["Password"]);
                client.EnableSsl = bool.Parse(smtpConfig["EnableSsl"]);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpConfig["FromEmail"], smtpConfig["FromName"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);
                client.Send(mailMessage);
            }
        }

        public void SendEmailToAdminsUsingTemplate(DefenseDbContext context, string firstName, string lastName, string email)
        {
            var admins = context.Users.Where(u => u.IdRole == 1 && u.Status).ToList();
            string subject = "Nuevo Usuario Registrado (Pendiente de Aprobación)";

            // Leer las rutas directamente desde el appsettings.json
            var templatePath = _configuration["EmailTemplates:TemplatePath"];

            if (string.IsNullOrWhiteSpace(templatePath) || !File.Exists(templatePath))
                throw new FileNotFoundException($"La plantilla de correo no se encontró en: {templatePath}");

            string templateContent = File.ReadAllText(templatePath);

            foreach (var admin in admins)
            {
                // Personalizar el contenido del correo para cada administrador
                string personalizedBody = templateContent
                    .Replace("{{AdminName}}", admin.FirstName)
                    .Replace("{{FirstName}}", firstName)
                    .Replace("{{LastName}}", lastName)
                    .Replace("{{Email}}", email)
                    .Replace("{{ApprovalLink}}", $"https://tusistema.com/approve?email={email}")
                    .Replace("{{RejectionLink}}", $"https://tusistema.com/reject?email={email}");

                SendEmail(admin.Email, subject, personalizedBody);
            }
        }
    }
}
