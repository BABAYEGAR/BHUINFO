using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Configuration;

namespace BhuInfo.Data.Service.EmailService
{
    public class MailerDaemon
    {
        /// <summary>
        ///     This method sends an email to a newly created user
        /// </summary>
        /// <param name="user"></param>
        public void NewUser(AppUser user)
        {
            var message = new MailMessage
            {
                From = new MailAddress(Config.DevEmailAddress, Config.DevEmailAddress),
                Subject = "New Password",
                Priority = MailPriority.High,
                SubjectEncoding = Encoding.UTF8,
                Body = GetEmailBody_NewUserCreated(user),
                IsBodyHtml = true
            };
            //message.To.Add(Config.DevEmailAddress);
            message.To.Add(user.Email);
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception exception)
            {
            }
        }

        /// <summary>
        ///     Html page content for the new user email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string GetEmailBody_NewUserCreated(AppUser user)
        {
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/NewUserCreated.html")).ReadToEnd()
                    .Replace("DISPLAYNAME", user.Firstname)
                    .Replace("URL", "")
                    .Replace("PASSWORD", user.Password);
        }

        ///     This method sends an email to a user who has forgotten his passwrd

        /// <summary>
        /// </summary>
        /// <param name="forgottenPasswordrequest"></param>
        //public void ResetUserPassword(ForgottenPasswordRequest forgottenPasswordrequest)
        //{
        //    var message = new MailMessage();

        //    message.From = new MailAddress(Config.SupportEmailAddress, Config.SupportDisplayName);
        //    message.To.Add(forgottenPasswordrequest.AppUser.Email);
        //    message.Subject = "New Password";
        //    message.Priority = MailPriority.High;
        //    message.SubjectEncoding = Encoding.UTF8;
        //    message.Body = GetEmailBody_NewPasswordCreated(forgottenPasswordrequest.AppUser);
        //    message.IsBodyHtml = true;
        //    try
        //    {

        //        var mailClient = new SmtpClient();
        //        mailClient.UseDefaultCredentials = false;
        //        mailClient.Credentials = new NetworkCredential("haruna@emergingplatforms.com", "Brigada95");
        //        mailClient.Port = 587;
        //        mailClient.Host = "smtp.office365.com";
        //        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        mailClient.EnableSsl = true;
        //        mailClient.Send(message);
        //    }
        //    catch (Exception exception)
        //    {
        //        // 
        //    }
        //}

        /// <summary>
        ///     This method contains contents for the forgot password html page
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //private static string GetEmailBody_NewPasswordCreated(AppUser user)
        //{
        //    return
        //        new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ForgotPassword.html"))
        //            .ReadToEnd()
        //            .Replace("DISPLAYNAME", user.Firstname)
        //            .Replace("URL","172.16.16.11/EPPortalBeta/ResetPassword.aspx?password="+user.Password);
        //}
    }
}