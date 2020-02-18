using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HoildayOptimizations.Integrations;
using HolidayOptimizations.Service.Controllers.Enums;
using HolidayOptimizations.Service.Entities.Features.Holidays;
using HolidayOptimizations.Service.Processes.Helpers;
using HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays;

namespace HolidayOptimizations.Service.Controllers.Helpers
{
    public class PublicHolidaysHelper : IHolidaysHelper
    {
        public IHolidaysRepository _repository;
        private INaggerClient _naggerClient;

        public PublicHolidaysHelper(IHolidaysRepository repository, INaggerClient naggerClient)
        {
            _repository = repository;
            _naggerClient = naggerClient;
        }

        public List<PublicHoliday> GetAllHolidays(long year)
        {
            var holidaysToInsert = new List<PublicHoliday>();
            var allHolidays = new List<PublicHoliday>();
            var holidaysFromDb = _repository.GetPulbicHolidaysByYear(year);
            foreach (var enumValue in Enum.GetValues(typeof(CountryCodesEnum)))
            {
                var holidays = new List<PublicHoliday>();
                if (holidaysFromDb.Any(x => x.CountryCode == enumValue.ToString() && x.Date.Year == year))
                {
                    holidays = holidaysFromDb.Where(x => x.CountryCode == enumValue.ToString()).ToList();
                }
                else
                {
                    holidays = _naggerClient.GetPublicHolidays(year, enumValue.ToString()).Result;
                    holidays.ForEach(x => x.EndDate = x.Date.AddHours(24));
                    holidaysToInsert.AddRange(holidays);
                }
                allHolidays.AddRange(holidays);

            }
            _repository.InsertHolidaysAsync(holidaysToInsert);

            return allHolidays;
        }
    }
}
