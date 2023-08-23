using MuniBarva.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES
{
    public class SendEmail : ISendEmail
    {
        private readonly SmtpClient smtpClient;
        private readonly string Server;
        private readonly string User;
        private readonly string Password;
        private readonly string Email;
        private readonly int Port;

        public SendEmail()
        {
            User = "rleiton22@hotmail.com";
            Password = "Andres221193@";
            Server = "smtp.office365.com";
            Port = 587;
            smtpClient = new SmtpClient();
            smtpClient.Port = Port;
            smtpClient.Host = Server;
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 10000;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(User, Password);
        }


        public async Task Send(string _to, string _subject, string _message)
        {
            try
            {
                var mailMessage = new MailMessage(User, _to, _subject, _message);

                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {

                throw new Exception("Lo sentimos ocurrió un error al intentar enviar el email, por favor vuelva a intertarlo, si el problema persite contacte al administrador del sistema.");
            }
            finally
            {
                smtpClient?.Dispose();
            }
        }
    }
}
