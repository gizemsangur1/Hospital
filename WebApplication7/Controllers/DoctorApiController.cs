using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication7.Models;
using WebApplication7.Utility;

namespace WebApplication7.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorApiController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorApiController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public IActionResult GetDoctorsBySpecialty(int bransId)
        {
            var doctors = _doctorRepository.GetAll(includeProps: "DoctorBrans")
               .Where(d => d.DoctorBrans.Id == bransId)
               .ToList();

            return Ok(doctors);
        }
    }
}
