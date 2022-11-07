using Api.Common.Interfaces.Helpers;
using System;

namespace Api.Common.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
