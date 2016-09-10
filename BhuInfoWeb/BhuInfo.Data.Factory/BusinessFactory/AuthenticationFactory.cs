using System;
using System.Data.Entity;
using System.Web.Security;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.EmailService;
using BhuInfo.Data.Service.Encryption;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class AuthenticationFactory
    {
        private readonly AppUserDataContext db = new AppUserDataContext();

        /// <summary>
        ///     This ,ethod is used to authenticate a users login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AppUser AuthenticateAppUserLogin(string email, string password, string role)
        {
            var hashPassword = new Md5Ecryption().ConvertStringToMd5Hash(password.Trim());
            var user = new AppUserFactory().GetAppUserByLogin(email, hashPassword, role);
            return user;
        }

        /// <summary>
        ///     This method is used to send a forgot password request to fetch the user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AppUser ForgotPasswordRequest(string email)
        {
            email = email.Trim();
            var user = new AppUserFactory().GetAppUserByEmail(email);
            var newPassword = Membership.GeneratePassword(8, 1);
            user.Password = newPassword;
            db.Entry(user).State = EntityState.Modified;
            new MailerDaemon().ResetUserPassword(user);
            db.SaveChanges();
            return user;
        }

        /// <summary>
        ///     This method is used to reset a user password
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="userId"></param>
        public void ResetUserPassword(string newPassword, int userId)
        {
            var user = new AppUserFactory().GetAppUserById(userId);
            user.Password = newPassword;
            var hashPasword = new Md5Ecryption().ConvertStringToMd5Hash(newPassword);
            db.Entry(user).State = EntityState.Modified;
            user.Password = hashPasword;
            db.SaveChanges();
        }

        /// <summary>
        ///     This method generates a password hash from a clear password using MD5
        /// </summary>
        /// <param name="clearPassword">The clear password to be hashed</param>
        /// <returns>The hashed password</returns>
        public string GetPasswordHash(string clearPassword)
        {
            return new Md5Ecryption().ConvertStringToMd5Hash(clearPassword);
        }

        /// <summary>
        ///     This ,ethod enables a user to change their password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangeUserPassword(long userId, string oldPassword, string newPassword)
        {
            var encryptedOldPassword = GetPasswordHash(oldPassword);
            if (encryptedOldPassword == null) throw new ArgumentNullException(nameof(encryptedOldPassword));
            var encryptedNewPassword = GetPasswordHash(newPassword);
            if (encryptedNewPassword == null) throw new ArgumentNullException(nameof(encryptedNewPassword));
            var isPasswordChanged = false;
            var user = db.AppUsers.Find(userId);
            if (user.Password == encryptedOldPassword)
            {
                user.Password = encryptedNewPassword;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                isPasswordChanged = true;
            }
            else
            {
                isPasswordChanged = false;
            }
            return isPasswordChanged;
        }
    }
}