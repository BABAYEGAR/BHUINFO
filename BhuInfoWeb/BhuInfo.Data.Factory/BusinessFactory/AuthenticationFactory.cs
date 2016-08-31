﻿using System.Data.Entity;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Encryption;

namespace BhuInfo.Data.Factory.BusinessFactory
{
   public  class AuthenticationFactory
    {
        private AppUserDataContext db = new AppUserDataContext();
        /// <summary>
        /// This ,ethod is used to authenticate a users login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AppUser AuthenticateAppUserLogin(string email, string password,string role)
        {
            var hashPassword  = new Md5Ecryption().ConvertStringToMd5Hash(password.Trim());
            var user = new AppUserFactory().GetAppUserByLogin(email, hashPassword,role);
            return user;

        }
/// <summary>
/// This method is used to send a forgot password request to fetch the user
/// </summary>
/// <param name="email"></param>
/// <returns></returns>
        public AppUser ForgotPasswordRequest(string email)
        {
            email = email.Trim();
            var user = new AppUserFactory().GetAppUserByEmail(email);
            var newPassword = System.Web.Security.Membership.GeneratePassword(8, 1);
            user.Password = newPassword;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return user;
        }
        /// <summary>
        /// This method is used to reset a user password
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="userId"></param>
        public void ResetUserPassword(string newPassword,int userId)
        {
            var user = new AppUserFactory().GetAppUserById(userId);
            user.Password = newPassword;
            var hashPasword = new Md5Ecryption().ConvertStringToMd5Hash(newPassword);
            db.Entry(user).State = EntityState.Modified;
            user.Password = hashPasword;
            db.SaveChanges();

        }

    }
}
