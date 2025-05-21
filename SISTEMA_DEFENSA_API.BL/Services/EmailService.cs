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
            string subject = "Nuevo Usuario Registrado - Pendiente de Aprobación";

            // Leer las rutas directamente desde el appsettings.json
            var templatePath = _configuration["EmailTemplates:NewUserTemplatePath"];
            var imageUrl = _configuration["EmailTemplates:ImageUrl"];

            var isProduction = bool.Parse(_configuration["EmailTemplates:IsProduction"]);
            var baseUrl = isProduction ? _configuration["EmailTemplates:BaseUrlProd"] : _configuration["EmailTemplates:BaseUrlDev"];

            if (string.IsNullOrWhiteSpace(templatePath) || !File.Exists(templatePath))
                throw new FileNotFoundException($"La plantilla de correo no se encontró en: {templatePath}");

            string templateContent = File.ReadAllText(templatePath);

            foreach (var admin in admins)
            {
                // Construir los endpoints de aprobación y rechazo de forma dinámica
                string approvalLink = $"{baseUrl}api/user/approve?email={email}";
                string rejectionLink = $"{baseUrl}api/user/reject?email={email}";

                // Personalizar el contenido del correo para cada administrador
                string personalizedBody = templateContent
                    .Replace("{{AdminName}}", admin.FirstName)
                    .Replace("{{FirstName}}", firstName)
                    .Replace("{{LastName}}", lastName)
                    .Replace("{{Email}}", email)
                    .Replace("{{ImageUrl}}", imageUrl)
                    .Replace("{{ApprovalLink}}", approvalLink)
                    .Replace("{{RejectionLink}}", rejectionLink);

                SendEmail(admin.Email, subject, personalizedBody);
            }
        }

        public void SendActionEmail(string to, string actionType, string firstName = "", string lastName = "")
        {
            var templatePath = _configuration["EmailTemplates:ActionTemplatePath"];
            var imageUrl = _configuration["EmailTemplates:ImageUrl"];

            var isProduction = bool.Parse(_configuration["EmailTemplates:IsProduction"]);
            var baseUrl = isProduction ? _configuration["EmailTemplates:BaseUrlProd"] : _configuration["EmailTemplates:BaseUrlDev"];
            var loginLink = $"{baseUrl}login";

            if (string.IsNullOrWhiteSpace(templatePath) || !File.Exists(templatePath))
                throw new FileNotFoundException($"La plantilla de correo no se encontró en: {templatePath}");

            string templateContent = File.ReadAllText(templatePath);

            // Determinar el contenido según el tipo de correo
            string emailTitle = "";
            string emailBody = "";

            if (actionType == "aprobado")
            {
                emailTitle = "Registro Aprobado";
                emailBody = $"Hola <strong>{firstName} {lastName}</strong>, tu cuenta ha sido aprobada. ¡Ya puedes iniciar sesión en el sistema con tus credenciales!";
            }
            else if (actionType == "rechazado")
            {
                emailTitle = "Registro Rechazado";
                emailBody = $"Hola <strong>{firstName} {lastName}</strong>, lamentamos informarte que tu registro ha sido rechazado. Si tienes dudas, contacta al administrador.";
            }

            string personalizedBody = templateContent
                .Replace("{{EmailTitle}}", emailTitle)
                .Replace("{{EmailBody}}", emailBody)
                .Replace("{{ImageUrl}}", imageUrl)
                .Replace("{{LoginLink}}", loginLink);

            SendEmail(to, emailTitle, personalizedBody);
        }
    }
}
