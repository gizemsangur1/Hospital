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
        private readonly IDoctorRepository _doktorRepository;
        private readonly IDoctorBransRepository _doktorBransRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        

        public DoctorController(IDoctorRepository doktorRepository, IDoctorBransRepository doktorBransRepository, IWebHostEnvironment webHostEnvironment)
        {
            _doktorRepository = doktorRepository;
            _doktorBransRepository = doktorBransRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin , Hasta")]
        public IActionResult Index()
        {


            List<Doctor> objDoktorList = _doktorRepository.GetAll(includeProps: "DoktorBrans").ToList();

            return View(objDoktorList);
        }



        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {


            IEnumerable<SelectListItem> DoktorBransList = _doktorBransRepository.GetAll().
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



                Doctor? doktorDb = _doktorRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

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

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(doktorPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                }



                if (doktor.Id == 0)
                {
                    _doktorRepository.Ekle(doktor);
                    TempData["basarili"] = "Doktor kayıt ekleme işlemi başarılı!";
                }
                else
                {
                    _doktorRepository.Guncelle(doktor);
                    TempData["basarili"] ="Doktor kayıt güncelleme işlemi başarılı!";
                }

                _doktorRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.



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



            Doctor? doktorDb = _doktorRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

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


            Doctor? doktor = _doktorRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (doktor == null)
            {
                return NotFound();
            }
            else
            {
                _doktorRepository.Sil(doktor);
                _doktorRepository.Kaydet();
                TempData["basarili"] = "Doktor kayıt silme işlemi başarılı!";
                return RedirectToAction("Index", "Doctor");
            }

        }
    }
}
