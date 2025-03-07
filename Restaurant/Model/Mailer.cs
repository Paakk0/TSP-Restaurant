using MimeKit;
using MailKit.Net.Smtp;
using System;

namespace Restaurant.Model
{
    public class Mailer
    {
        private static string sender { get; set; } = "beko1972@abv.bg";
        private static string password { get; set; } = "password";
        private static Dictionary<string, string> Messages { get; set; } = new Dictionary<string, string>()
        {
            { "Received", "Your order has been received and is being processed." },
            { "Completed", "Your order has been completed and is on its way." }
        };

        public static void SendMail(User receiver, string subject, int orderId)
        {
            return;
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Restaurant", sender));
                message.To.Add(new MailboxAddress(receiver.Name, receiver.Email));
                message.Subject = $"Order {orderId} {subject}";

                message.Body = new TextPart("plain")
                {
                    Text = Messages.GetValueOrDefault(subject)
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.abv.bg", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
                    client.Authenticate(sender, password);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
