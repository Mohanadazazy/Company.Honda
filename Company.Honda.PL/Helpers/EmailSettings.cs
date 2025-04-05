using System.Net;
using System.Net.Mail;

namespace Company.Honda.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            // mail Server : Gmail
            // SMTP

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("mohanadazazy@gmail.com", "taaicppqbbdkztfq");
                client.Send("mohanadazazy@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
