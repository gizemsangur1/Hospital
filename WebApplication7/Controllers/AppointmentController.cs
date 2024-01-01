using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Host;
using WebApplication7.Models;
using WebApplication7.Utility;

namespace WebApplication7.Controllers
{
    public class AppointmentController : Controller
    {

        private readonly IAppointmentRepository _randevuRepository;
        private readonly IDoctorRepository _doktorRepository;
        private readonly IDoctorBransRepository _doktorBransRepository;

        public readonly IWebHostEnvironment _webHostEnvironment;

        public AppointmentController(IAppointmentRepository randevuRepository,IDoctorBransRepository doktorBransRepository ,IDoctorRepository doktorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _randevuRepository = randevuRepository;
            _doktorRepository = doktorRepository;
            _doktorBransRepository = doktorBransRepository;

            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin, Patient")]
        public IActionResult Index()
        {


            List<Appointment> objRandevuList = _randevuRepository.GetAll(includeProps: "Doctor").ToList();

            return View(objRandevuList);
        }



        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {

            IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.DoctorName + k.WorkingTimes,
                    Value = k.Id.ToString(),
                }
               );

            ViewBag.DoktorList = DoktorList;


            if (id == null || id == 0)//ekle
            {

                return View();

            }
            else
            {//güncelle



                Appointment? randevuDb = _randevuRepository.Get(u => u.Id == id);

                if (randevuDb == null)
                {


                    return NotFound();

                }
                return View(randevuDb);
            }
        }



        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult EkleGuncelle(Appointment randevu)
        {

            if (ModelState.IsValid)
            {


                if (randevu.Id == 0)
                {
                    _randevuRepository.Ekle(randevu);
                    TempData["basarili"] = "Yeni randevu kaydedildi!";
                }
                else
                {
                    _randevuRepository.Guncelle(randevu);
                    TempData["basarili"] = "Randevu güncellendi!";
                }

                _randevuRepository.Kaydet();



                return RedirectToAction("Index", "Appointment");
            }
            else
            {
                return View();
            }


        }



        [Authorize(Roles = UserRoles.Role_Patient)]
        public IActionResult RandevuAl()
        {

            var loggedInUserName = User.Identity.Name;

            var bransName = _doktorBransRepository.GetAll()
            .Select(b => new SelectListItem
            {
                Text = b.Ad,
                Value = b.Id.ToString(),
            });



            var availableDoctors = _doktorRepository.GetAll()
    .Where(d => !_randevuRepository.GetAll().Any(a => a.DoctorId == d.Id))
    .Select(k => new SelectListItem
    {
        Text = k.DoctorName + k.WorkingTimes,
        Value = k.Id.ToString(),
    });
            ViewBag.BransList = bransName;

            ViewBag.DoktorList = availableDoctors;

            var appointment = new Appointment
            {
                PatientName = loggedInUserName,
            };

            return View(appointment);
        }


        [Authorize(Roles = UserRoles.Role_Patient)]
        [HttpPost]
        public IActionResult RandevuAl(Appointment randevu)
        {
            if (ModelState.IsValid)
            {
                randevu.Id = 0;
                _randevuRepository.Ekle(randevu);
                TempData["basarili1"] = "Yeni randevu alındı!";
                _randevuRepository.Kaydet();
                return RedirectToAction("Index", "Appointment");
            }
            else
            {
                return View();
            }
        }


        [Authorize(Roles = "Admin, Patient")]

        public IActionResult Sil(int? id)
        {

            IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.DoctorName,
                    Value = k.Id.ToString(),
                }
               );

            ViewBag.DoktorList = DoktorList;


            if (id == null || id == 0)
            {

                return NotFound();

            }


            Appointment? randevuDb = _randevuRepository.Get(u => u.Id == id);

            if (randevuDb == null)
            {


                return NotFound();

            }


            return View(randevuDb);
        }



        [Authorize(Roles = "Admin, Patient")]

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {


            Appointment? randevu = _randevuRepository.Get(u => u.Id == id);

            if (randevu == null)
            {
                return NotFound();
            }
            else
            {
                _randevuRepository.Sil(randevu);
                _randevuRepository.Kaydet();
                TempData["basarili"] = "Randevu silindi!";
                return RedirectToAction("Index", "Appointment");
            }
        }


    }
}