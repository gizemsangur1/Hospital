using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.Utility;

namespace WebApplication7.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
      
    public class DoctorBransController : Controller
    {
        
            private readonly IDoctorBransRepository _doctorBransRepository;

            public DoctorBransController(IDoctorBransRepository context)
            {
                _doctorBransRepository = context;
            }


            public IActionResult Index()
            {
                List<DoctorBrans> objDoktorBransList = _doctorBransRepository.GetAll().ToList();

                return View(objDoktorBransList);
            }


            public IActionResult Ekle()
            {

                return View();
            }




            [HttpPost]
            public IActionResult Ekle(DoctorBrans doctorBransEkle)
            {
                if (ModelState.IsValid)
                {
                    _doctorBransRepository.Ekle(doctorBransEkle);
                    _doctorBransRepository.Kaydet();

                    TempData["basarili"] = "Doktor Branşı ekleme işlemi başarılı!";

                    return RedirectToAction("Index", "DoctorBrans");
                }
                else
                {
                    return View();
                }


            }


            public IActionResult Guncelle(int? id)
            {


                if (id == null || id == 0)
                {

                    return NotFound();

                }





                DoctorBrans? doktorBransDb = _doctorBransRepository.Get(u => u.Id == id);

                if (doktorBransDb == null)
                {


                    return NotFound();

                }


                return View(doktorBransDb);
            }





            [HttpPost]
            public IActionResult Guncelle(DoctorBrans doktorBransGuncelle)
            {


                if (ModelState.IsValid)
                {
                    _doctorBransRepository.Guncelle(doktorBransGuncelle);
                    _doctorBransRepository.Kaydet();

                    TempData["basarili"] = "Doktor Branşı güncelleme işlemi başarılı!";

                    return RedirectToAction("Index", "DoctorBrans");
                }
                else
                {

                    return View();
                }
            }




            public IActionResult Sil(int? id)
            {


                if (id == null || id == 0)
                {

                    return NotFound();

                }



                DoctorBrans? doktorBransDb = _doctorBransRepository.Get(u => u.Id == id);

                if (doktorBransDb == null)
                {


                    return NotFound();

                }


                return View(doktorBransDb);
            }




            [HttpPost, ActionName("Sil")]
            public IActionResult SilPOST(int? id)
            {


                DoctorBrans? doktorBrans = _doctorBransRepository.Get(u => u.Id == id);
                if (doktorBrans == null)
                {
                    return NotFound();
                }
                else
                {
                    _doctorBransRepository.Sil(doktorBrans);
                    _doctorBransRepository.Kaydet();
                    TempData["basarili"] = "Doktor Branşı silme işlemi başarılı!";
                    return RedirectToAction("Index", "DoctorBrans");
                }
            }
        
    }
}

