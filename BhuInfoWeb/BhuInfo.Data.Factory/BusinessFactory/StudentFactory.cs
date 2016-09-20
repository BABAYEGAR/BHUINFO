using System;
using System.Linq;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class StudentFactory
    {
        private readonly StudentDataContext _db = new StudentDataContext();

        /// <summary>
        ///     This method finds a yuser with the provided password and email/matric
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="matricNo"></param>
        /// <returns></returns>
        public Student GetStudentByLogin(string email, string password, string matricNo)
        {
            email = email.Trim();
            var appUser =
                _db.Students.FirstOrDefault(n => (n.Email == email) || n.MatricNo == matricNo && (n.Password == password));
            return appUser;
        }

        /// <summary>
        ///     This method checks if a user exist
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckIfStudentExist(string email,string matricNo)
        {
            var userExist = false;
            try
            {
                var allUsers = _db.Students;
                if (allUsers.Any(n => n.Email == email || n.MatricNo == matricNo))
                    userExist = true;
            }
            catch (Exception)
            {
                // ignored
            }
            return userExist;
        }

        /// <summary>
        ///     This method is used to retrieve a user by user email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="matricNo"></param>
        /// <returns></returns>
        public Student GetStudentByEmailOrMatricNo(string email,string matricNo)
        {
            email = email.Trim();
            var appUser = _db.Students.Single(n => n.Email == email || n.MatricNo == matricNo);
            return appUser;
        }

        /// <summary>
        ///     This method retrives a user by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student GetStudentById(int id)
        {
            var appUser = _db.Students.Find(id);
            return appUser;
        }
    }
}