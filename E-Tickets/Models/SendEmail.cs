using E_Tickets.Models.ViewModels;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace E_Tickets.Models
{
    public class SendEmail
    {
        static public string SendAnEmail(EmailViewModel emailContent)
        {

            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential("try.trying303@gmail.com", "try_tring123"),
                EnableSsl = true,
            });

            Email.DefaultSender = sender;

            var email = Email.From(emailContent.Email)
                              .To("try.trying303@gmail.com")
                              .Subject(emailContent.Subject)
                              .Body("This Email From "+ emailContent.Email + " His/Her Name " + emailContent.Name + ", " + emailContent.Massage);
            
            var result = email.Send();


            if (result.Successful)
            {
                return "The email sent successfully";
            }
            else
            {
                return "Error just occurred";
            }
        }
    }
}
