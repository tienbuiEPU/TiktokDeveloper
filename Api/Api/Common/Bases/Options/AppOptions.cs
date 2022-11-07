using static Api.Common.Enums.AppEnums;

namespace Api.Common.Bases.Options
{
    public class AppOptions
    {
        public ShowLogLevel ShowLogLevel { get; set; }
        public string SiteURL { get; set; }
        public string EmailConfirmationExpiryTime { get; set; }
    }
}
