using HolidayOptimizations.Service.Processes;
using HolidayOptimizations.Web.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace HolidayOptimizations.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private IHolidaysProcess _process; 

        public HolidaysController(IHolidaysProcess process)
        {
            _process = process;
        }

        /// <summary>
        /// Name of the country that has most public holidays globally
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error" ,typeof(BadRequestObjectResult))]
        [SwaggerResponse(StatusCodes.Status200OK, "Success" ,typeof(string))]
        [HttpGet("{year?}")]
        [Throttle(Name = "GetCountryWithMostHolidays", Milliseconds = 200)]
        public ActionResult GetCountryWithMostHolidays(long year = 0)
        {
            year = year != 0 ? year : DateTime.Now.Year;

            var response = _process.GetCountryWithMostHolidays(year);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var countryWithMostHolidays = response.Response;

                return Ok(countryWithMostHolidays);

            }
            else
            {
                return BadRequest(400);
            }
        }

        /// <summary>
        /// Name of the month that has most public holidays globally
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error", typeof(BadRequestObjectResult))]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(string))]
        [HttpGet("{year?}")]
        [Throttle(Name = "GetMostHolidaysByMonth", Milliseconds = 200)]
        public ActionResult GetMostHolidaysByMonth(long year = 0)
        {
            year = year != 0 ? year : DateTime.Now.Year;

            var response = _process.GetMostHolidaysByMonth(year);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var monthWithMostHolidays = response.Response;

                return Ok(monthWithMostHolidays);

            }
            else
            {
                return BadRequest(400);
            }
        }

        /// <summary>
        /// Country that has the most unique public holidays
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error", typeof(BadRequestObjectResult))]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(string))]
        [HttpGet("{year?}")]
        [Throttle(Name = "GetCountryWithMostUniqueHolidays", Milliseconds = 200)]
        public ActionResult GetCountryWithMostUniqueHolidays(long year = 0)
        {
            year = year != 0 ? year : DateTime.Now.Year;

            var response = _process.GetCountryWithMostUniqueHolidays(year);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var monthWithMostHolidays = response.Response;

                return Ok(monthWithMostHolidays);

            }
            else
            {
                return BadRequest(400);
            }
        }

        /// <summary>
        /// Longest lasting sequence of holidays around the world
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error", typeof(BadRequestObjectResult))]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(string))]
        [HttpGet("{year?}")]
        [Throttle(Name = "LightSpeedTravel", Milliseconds = 200)]
        public ActionResult LightSpeedTravel(long year = 0)
        {
            year = year != 0 ? year : DateTime.Now.Year;

            var response = _process.LightSpeedTravel(year);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var longestSequence = response.Response;

                return Ok(longestSequence);

            }
            else
            {
                return BadRequest(400);
            }
        }
    }
}
