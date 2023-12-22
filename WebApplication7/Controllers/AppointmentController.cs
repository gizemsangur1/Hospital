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
            public readonly IWebHostEnvironment _webHostEnvironment;

            public AppointmentController(IAppointmentRepository randevuRepository, IDoctorRepository doktorRepository, IWebHostEnvironment webHostEnvironment)
            {
                _randevuRepository = randevuRepository;
                _doktorRepository = doktorRepository;
                _webHostEnvironment = webHostEnvironment;
            }

            [Authorize(Roles = UserRoles.Role_Admin)]
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
                        Text = k.DoctorName,
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



                    Appointment? randevuDb = _randevuRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

                    if (randevuDb == null)
                    {


                        return NotFound();

                    }
                    return View(randevuDb);
                }
            }



            [Authorize(Roles = UserRoles.Role_Admin)]
            [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
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
                        TempData["basarili"] ="Randevu güncellendi!";
                    }

                    _randevuRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.



                    return RedirectToAction("Index", "Appointment");
                }
                else
                {
                    return View();
                }


            }






            [Authorize(Roles = UserRoles.Role_Patient)]
            public IActionResult RandevuAl(int? id)
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


                // if (id == null || id == 0)//ekle
                //{

                return View();

                // }

            }


            [Authorize(Roles = UserRoles.Role_Patient)]
            [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
            public IActionResult RandevuAl(Appointment randevu)
            {

                if (ModelState.IsValid)
                {

                    randevu.Id = 0;


                    _randevuRepository.Ekle(randevu);
                    TempData["basarili1"] = "Yeni randevu alındı!";



                    _randevuRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.



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


                Appointment? randevuDb = _randevuRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

                if (randevuDb == null)
                {


                    return NotFound();

                }


                return View(randevuDb);
            }



            [Authorize(Roles = UserRoles.Role_Admin)]
            [HttpPost, ActionName("Sil")]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
            public IActionResult SilPOST(int? id)
            {


                Appointment? randevu = _randevuRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

                if (randevu == null)
                {
                    return NotFound();
                }
                else
                {
                    _randevuRepository.Sil(randevu);
                    _randevuRepository.Kaydet();
                    TempData["basarili"] ="Randevu silindi!";
                    return RedirectToAction("Index", "Appointment");
                }
            }

        
    }
}
