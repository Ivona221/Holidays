using HolidayOptimizations.Service.Entities.Features.Holidays;
using HolidayOptimizations.Service.Entities.Reponse;
using HolidayOptimizations.Service.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HoildayOptimizations.Integrations;
using HolidayOptimizations.Service.Controllers;
using HolidayOptimizations.Service.Processes.Helpers;
using HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays;
using HolidayOptimizations.Service.Processes.Logger;

namespace HolidayOptimizations.TestMS
{
    [TestClass]
    public class HolidayTests
    {
        private Mock<IHolidaysHelper> holidaysHelper;
        private Mock<ITimezonesRepository> timezonesRepository;
        private Mock<INaggerClient> naggerClient;
        private Mock<ILogger> logger;
        private HolidaysService holidaysController;

        [TestInitialize]
        public void TestInitialize()
        {
            holidaysHelper = new Mock<IHolidaysHelper>();
            timezonesRepository = new Mock<ITimezonesRepository>();
            naggerClient = new Mock<INaggerClient>();
            logger = new Mock<ILogger>();

            holidaysController = new HolidaysService(holidaysHelper.Object, 
                                                     timezonesRepository.Object,
                                                     naggerClient.Object, 
                                                     logger.Object);

            holidaysHelper.Setup(x => x.GetAllHolidays(2020)).Returns(new List<PublicHoliday>
            {
                new PublicHoliday
                {
                    Name = "New Year's Day",
                    LocalName = "Any nou",
                    CountryCode = "AD",
                    Fixed = true,
                    Global = true,
                    Countries = null,
                    LaunchYear = null,
                    ModifiedAt = DateTime.MinValue,
                    CreatedAt = DateTime.MinValue,
                    EndDate = Convert.ToDateTime("11/18/2020 1:00:00 PM"),
                    Date = Convert.ToDateTime("11/17/2020 8:00:00 AM")
                },
                new PublicHoliday
                {
                    Name = "Feria of La Chinita",
                    LocalName = "Feria de la Chinita",
                    CountryCode = "VE",
                    Fixed = true,
                    Global = true,
                    Countries = null,
                    LaunchYear = null,
                    ModifiedAt = DateTime.MinValue,
                    CreatedAt = DateTime.MinValue,
                    EndDate = Convert.ToDateTime("11/18/2020 1:00:00 PM"),
                    Date = Convert.ToDateTime("11/17/2020 8:00:00 AM")
                }
            });
            var timezonesList = new List<Timezone>
            {
                new Timezone
                {
                    TimezoneUTC = "UTC-04:00",
                    CountryCode = "VE"
                },
                new Timezone
                {
                    TimezoneUTC = "UTC+01:00",
                    CountryCode = "AD"
                }
            };
            timezonesRepository.Setup(x => x.GetAllTimezones()).Returns(timezonesList);
            var returnCountry = Task.FromResult<Country>(new Country
            {
                CommonName = "Venezuela",
                OfficialName = "Bolivarian Republic of Venezuela",
                CountryCode = "VE",
                AlternativeSpellings = new List<string>() {
                    "فنزويلا",
                    "Venesuela"

                },
                Region = "Americas",
                Borders = new List<Country>() { }
            });
            timezonesRepository.Setup(x => x.InsertTimezonesAsync(timezonesList));
            
            naggerClient.Setup(x => x.GetCountryInfo("AD")).Returns(returnCountry);
        }

        [TestMethod]
        public void HolidayController_GetCountryWithMostHolidays_Should_Return_Ok_Result()
        {
            // arange
            var year = 2020;

            // act
            var actionResult = holidaysController.GetCountryWithMostHolidays(year);
            var okResult = actionResult as BaseResponse<string>;

            // assert
            Assert.AreEqual(HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual("Venezuela", okResult.Response);
        }

        [TestMethod]
        public void HolidayController_GetMostHolidaysByMonth_Should_Return_Ok_Result()
        {
            // arange
            var year = 2020;

            // act
            var actionResult = holidaysController.GetMostHolidaysByMonth(year);
            var okResult = actionResult as BaseResponse<string>;

            // assert
            Assert.AreEqual(HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual("November", okResult.Response);
        }

        [TestMethod]
        public void HolidayController_GetCountryWithMostUniqueHolidays_Should_Return_Ok_Result()
        {
            // arange
            var year = 2020;

            // act
            var actionResult = holidaysController.GetCountryWithMostUniqueHolidays(year);
            var okResult = actionResult as BaseResponse<string>;

            // assert
            Assert.AreEqual(HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual("Bolivarian Republic of Venezuela", okResult.Response);
        }

        [TestMethod]
        public void HolidayController_LightSpeedTravel_Should_Return_Ok_Result()
        {
            // arange
            var year = 2020;
            var lightSpeedResponse = new LightspeedTravelResponse
            {
                LongestSequence = 1,
                Holidays = new List<PublicHoliday>()
                {
                    new PublicHoliday
                    {
                        Name = "New Year's Day",
                        LocalName = "Any nou",
                        CountryCode = "AD",
                        Fixed = true,
                        Global = true,
                        Countries = null,
                        LaunchYear = null,
                        ModifiedAt = DateTime.MinValue,
                        CreatedAt = DateTime.MinValue,
                        EndDate = Convert.ToDateTime("11/18/2020 1:00:00 PM"),
                        Date = Convert.ToDateTime("11/17/2020 8:00:00 AM"),
                        Id = 0
                    }
                }
            };

            // act
            var actionResult = holidaysController.LightSpeedTravel(year);
            var okResult = actionResult as BaseResponse<LightspeedTravelResponse>;

            // assert
            Assert.AreEqual(HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(lightSpeedResponse.LongestSequence, okResult.Response.LongestSequence);
        }
    }
}
