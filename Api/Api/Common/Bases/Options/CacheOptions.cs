using static Api.Common.Enums.AppEnums;

namespace Api.Common.Bases.Options
{
    public class CacheOptions
    {
        public int CacheTime { get; set; }
        public CacheDataTypes Type { get; set; }
    }
}
