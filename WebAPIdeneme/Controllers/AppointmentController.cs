using Business.Abstract;
using Business.CrossCuttingConcerns;
using Core.Aspects.AutofacAspect.ExceptionLog;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("add")]
        [ExceptionLogAspect(typeof(ErrorDatabaseLogger))]
        public IActionResult Add(AppointmentEntity appointment)
        {
            var result = _appointmentService.Add(appointment);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
