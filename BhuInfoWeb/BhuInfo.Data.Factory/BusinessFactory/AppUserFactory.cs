using System;
using System.Linq;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class AppUserFactory
    {
        private readonly AppUserDataContext db = new AppUserDataContext();

        /// <summary>
        ///     This method finds a yuser with the provided password and email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public AppUser GetAppUserByLogin(string email, string password, string role)
        {
            email = email.Trim();
            var appUser =
                db.AppUsers.FirstOrDefault(n => (n.Email == email) && (n.Password == password) && (n.Role == role));
            return appUser;
        }

        /// <summary>
        ///     This method checks if a user exist
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckIfUserExist(string email)
        {
            var userExist = false;
            try
            {
                var allUsers = db.AppUsers;
                if (allUsers.Any(n => n.Email == email))
                    userExist = true;
            }
            catch (Exception ex)
            {
            }
            return userExist;
        }

        /// <summary>
        ///     This method is used to retrieve a user by user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AppUser GetAppUserByEmail(string email)
        {
            email = email.Trim();
            var appUser = db.AppUsers.Single(n => n.Email == email);
            return appUser;
        }

        /// <summary>
        ///     This method retrives a user by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AppUser GetAppUserById(int id)
        {
            var appUser = db.AppUsers.Find(id);
            return appUser;
        }
    }
}