using System.Configuration;

namespace BhuInfo.Data.Service.Configuration
{
    public static class Config
    {
        public static string SupportEmailAddress => "bhuinfo@support.com";
        //public static string SupportDisplayName => ConfigurationManager.AppSettings["SupportEmailDisplayName"];

        //public static string SupportEmailAddress => ConfigurationManager.AppSettings["SupportEmail"];
    }
}