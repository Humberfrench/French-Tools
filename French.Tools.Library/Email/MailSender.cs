using French.Tools.Extensions;
using System;
using System.Net;
using System.Net.Mail;
using WebGrease.Css.Extensions;
using static French.Tools.Extensions.AppSettings;
namespace French.Tools.Library.Email
{
    public class MailSender
    {
        public string Credencial { get; }
        public string Key { get; }
        public string EmailSender { get; }
        public string Smtp { get; }
        public string Erro { get; set; }
        public bool EnvioEmailAdicional { get; set; }
        public string EmailAdicional1 { get; set; }
        public string EmailAdicional2 { get; set; }

        public MailSender()
        {
            Credencial = Get("Credencial");
            Key = Get("Key");
            EmailSender = Get("EmailSender");
            Smtp = Get("Smtp");
            EnvioEmailAdicional = GetBoolean("EnvioEmailAdicional");
            EmailAdicional1 = Get("EmailAdicional1");
            EmailAdicional2 = Get("EmailAdicional2");
        }

        public bool EnviarEmail(string email, string assunto, string textoemail, string bcc ="")
        {
            bool blnReturn = true;

            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(EmailSender),
                    Subject = assunto,
                    IsBodyHtml = true,
                    Body = textoemail,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
                    Priority = MailPriority.High,
                    Bcc =
                    {
                        "pagamentos@French.com.br"
                    }
                };

                if (!bcc.IsNullOrEmptyOrWhiteSpace())
                {

                    if (bcc.Contains(";"))
                    {
                        var bccs = bcc.Split(';');
                        bccs.ForEach(b => mail.Bcc.Add(b));
                    }
                    else
                    {
                        mail.Bcc.Add(bcc);
                    }
                    
                }

                if (EnvioEmailAdicional)
                {
                    if (!EmailAdicional1.IsNullOrEmptyOrWhiteSpace())
                    {
                        mail.Bcc.Add(EmailAdicional1);
                    }
                    if (!EmailAdicional2.IsNullOrEmptyOrWhiteSpace())
                    {
                        mail.Bcc.Add(EmailAdicional2);
                    }
                }

                mail.To.Add(email);

                using (var smtp = new SmtpClient(Smtp))
                {
                   
                    //smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false; // vamos utilizar credencias especificas
                    smtp.DeliveryFormat = SmtpDeliveryFormat.International;
                    smtp.Credentials = new NetworkCredential(Credencial, Key);
                    smtp.Send(mail);
                }

            }
            catch (Exception ex)
            {
                Erro = ex.Message;
                blnReturn = false;
            }

            return blnReturn;
        }
        public bool EnviarEmailLocal(string email, string assunto, string textoemail)
        {
            bool blnReturn = true;

            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress("172.31.19.225"),
                    Subject = assunto,
                    IsBodyHtml = true,
                    Body = textoemail,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
                    Priority = MailPriority.High,
                    Bcc =
                    {
                        "pagamentos@French.com.br",
                        "humberto.almeida@French.com.br",
                        "andre@French.com.br"
                    }
                };


                mail.To.Add(email);


                using (var smtp = new SmtpClient(Smtp))
                {
                    //smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false; // vamos utilizar credencias especificas

                    smtp.Credentials = new NetworkCredential(Credencial, Key);

                    smtp.Send(mail);
                }

            }
            catch (Exception ex)
            {
                Erro = ex.Message;
                blnReturn = false;
            }

            return blnReturn;
        }

    }
}