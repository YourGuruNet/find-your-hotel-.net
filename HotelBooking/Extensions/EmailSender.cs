using System.Net;
using System.Net.Mail;
using HotelBooking.Models;

public class EmailSender
{
    public static void SendEmail(string to, string subject, string body)
    {
        var fromAddress = new MailAddress(Settings.Mail, "Hotel Booking");
        var toAddress = new MailAddress(to);
        var smtpClient = new SmtpClient
        {
            Host = Settings.Host,
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(Settings.Mail, "qQndz1VhFMGwAxUf")
        };

        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };
        smtpClient.Send(message);
    }
}