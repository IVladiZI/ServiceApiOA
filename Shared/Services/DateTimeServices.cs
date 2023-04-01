using Application.Interfaces;

namespace Shared.Services
{
    /// <DateTimeServices>
    /// The DateTimeService is implemented to make the correct dependency injection of this service.
    /// </DateTimeServices>
    public class DateTimeServices : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
