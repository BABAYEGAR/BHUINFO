using System;
using System.Linq;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Factory.BusinessFactory
{
    public class AdvertsiementFactory
    {
        private readonly AdvertisementDataContext _db = new AdvertisementDataContext();
        /// <summary>
        ///     This method checks if a an advert type exist
        /// </summary>
        /// <param name="advertType"></param>
        /// <param name="status"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckIfAdvertTypeExist(string advertType,string status,string email)
        {
            var advertExist = false;
            try
            {
                var allAdverts = _db.Advertisements;
                if (allAdverts.Any(n => n.AdvertType == advertType && n.Status == status && n.Email == email))
                    advertExist = true;
            }
            catch (Exception)
            {
                // ignored
            }
            return advertExist;
        }
        /// <summary>
        /// This method checks if an acces code exist
        /// </summary>
        /// <param name="accessCode"></param>
        /// <returns></returns>
        public Advertisement CheckIfAccessCodeExist(string accessCode)
        {
            Advertisement singleAdvert = null;
            try
            {
                var allAdverts = _db.Advertisements;
                singleAdvert = allAdverts.FirstOrDefault(n => n.AccessCode == accessCode);
            }
            catch (Exception)
            {
                // ignored
            }
            return singleAdvert ;
        }

        /// <summary>
        ///     This method retrives an advert by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Advertisement GetAdvertById(int id)
        {
            var advert = _db.Advertisements.Find(id);
            return advert;
        }
    }
}