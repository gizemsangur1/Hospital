using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Host;
using WebApplication7.Models;
using WebApplication7.Utility;

namespace WebApplication7.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorBransRepository _doctorBransRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        

        public DoctorController(IDoctorRepository doctorRepository, IDoctorBransRepository doctorBransRepository, IWebHostEnvironment webHostEnvironment)
        {
            _doctorRepository = doctorRepository;
            _doctorBransRepository = doctorBransRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin, Patient")]
        public IActionResult Index()
        {
            List<Doctor> objDoktorList = _doctorRepository.GetAll(includeProps: "DoctorBrans").ToList();
            return View(objDoktorList);
        }

        [HttpGet]
        public IActionResult GetDoctorsBySpecialty(string brans)
        {
            var doctors = _doctorRepository.GetAll()
                .Where(d => d.DoctorBrans.Id.ToString() == brans)
                .Select(d => new { value = d.Id, text = d.DoctorName });

            return Json(doctors);
        }



        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {


            IEnumerable<SelectListItem> DoktorBransList = _doctorBransRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString(),
                }
               );

            ViewBag.DoktorBransList = DoktorBransList;
            if (id == null || id == 0)//ekle
            {

                return View();

            }
            else
            {//güncelle



                Doctor? doktorDb = _doctorRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

                if (doktorDb == null)
                {


                    return NotFound();

                }
                return View(doktorDb);
            }
        }





        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(Doctor doktor, IFormFile? file)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);

            if (ModelState.IsValid)
            {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string doktorPath = Path.Combine(wwwRootPath, @"img");
                doktor.WorkingTimes = DateTime.SpecifyKind(doktor.WorkingTimes, DateTimeKind.Local);
                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(doktorPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                }



                if (doktor.Id == 0)
                {
                    _doctorRepository.Ekle(doktor);
                    TempData["basarili"] = "Doktor kayıt ekleme işlemi başarılı!";
                }
                else
                {
                    _doctorRepository.Guncelle(doktor);
                    TempData["basarili"] ="Doktor kayıt güncelleme işlemi başarılı!";
                }

                _doctorRepository.Kaydet();



                return RedirectToAction("Index", "Doctor");
            }
            else
            {
                return View();
            }


        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();

            }



            Doctor? doktorDb = _doctorRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (doktorDb == null)
            {


                return NotFound();

            }


            return View(doktorDb);
        }


        [HttpPost, ActionName("Sil")]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult SilPOST(int? id)
        {


            Doctor? doktor = _doctorRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (doktor == null)
            {
                return NotFound();
            }
            else
            {
                _doctorRepository.Sil(doktor);
                _doctorRepository.Kaydet();
                TempData["basarili"] = "Doktor kayıt silme işlemi başarılı!";
                return RedirectToAction("Index", "Doctor");
            }

        }
    }
}
