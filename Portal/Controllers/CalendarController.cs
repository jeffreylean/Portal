using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portal.Models;
using Portal.Services;
using Newtonsoft.Json;
using System.Web;

namespace Portal.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly ICalendarService calendarService;

        public CalendarController(ILogger<CalendarController> logger,ICalendarService calendarService)
        {
            _logger = logger;
            this.calendarService = calendarService;
        }
        public IActionResult Calendar()
        {
            return View("~/Views/Calendar/Calendar.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public void InsertNewEvent([FromBody]CalendarInfo data)
        {
            calendarService.InsertNewEvent(data);
        }
        [HttpGet]
        public JsonResult GetEventList()
        {
            var events = calendarService.GetEventList();
            return Json(events);
        }
        [HttpPut]
        public IActionResult UpdateEvent([FromBody]CalendarInfo data)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Not Valid Model");
            }
            calendarService.UpdateEvent(data);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteEvent([FromBody]CalendarInfo data)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Not a Valid Model");
            }
            calendarService.DeleteEvent(data);
            return Ok();
        }
                
    }
}
