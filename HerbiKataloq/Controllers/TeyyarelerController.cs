using HerbiKataloq.Common;
using HerbiKataloq.Models;
using HerbiKataloq.Models.TeyyareModels;
using HerbiKataloq.Services.TeyyareServices;
using HerbiKataloq.ViewModels;
using HerbiKataloq.ViewModels.Teyyare;
using Microsoft.AspNetCore.Mvc;

namespace HerbiKataloq.Controllers
{
    public class TeyyarelerController : Controller
    {
        private readonly ITeyyareService _teyyareService;

        public TeyyarelerController(ITeyyareService teyyareService)
        {
            _teyyareService = teyyareService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string category = null)
        {
            
            List<IndexTeyyareVM> teyyarelerVM = [];


            if (category is not null)
            {
                ViewBag.Title = "Təyyarələr - " + category;
                ViewBag.PageName = "teyyareler";

                List<Teyyare> teyyarelerByCategory = await _teyyareService.GetAllByCategory(category);

                foreach (Teyyare teyyare in teyyarelerByCategory)
                {
                    teyyarelerVM.Add(new IndexTeyyareVM()
                    {
                        Id = teyyare.Id.ToString(),
                        Adi = teyyare.Adi ?? " ",
                        Tipi = teyyare.Tipi ?? " ",
                        Foto = Helpers.ConvertPhotoToBase64(teyyare.Foto),
                    });
                }

                return View(teyyarelerVM);
            }

            List<Teyyare> teyyareler = await _teyyareService.GetAllAsync();

            ViewBag.Title = "Təyyarələr";
            ViewBag.PageName = "teyyareler";

            foreach (Teyyare teyyare in teyyareler)
            {
                teyyarelerVM.Add(new IndexTeyyareVM()
                {
                    Id = teyyare.Id.ToString(),
                    Adi = teyyare.Adi ?? " ",
                    Tipi = teyyare.Tipi ?? " ",
                    Foto = Helpers.ConvertPhotoToBase64(teyyare.Foto),
                });
            }

            return View(teyyarelerVM);
        }

        [HttpGet]
        public async Task<IActionResult> Axtar(string keyOfTeyyare, string valueOfTeyyare)
        {
            if (string.IsNullOrEmpty(keyOfTeyyare) || string.IsNullOrEmpty(valueOfTeyyare)) return NotFound();

            List<Teyyare> teyyareler = await _teyyareService.Find(keyOfTeyyare, valueOfTeyyare);

            if (teyyareler is null && teyyareler.Count == 0) return NotFound();

            List<IndexTeyyareVM> teyyarelerVM = [];

            foreach (Teyyare teyyare in teyyareler)
            {
                teyyarelerVM.Add(new IndexTeyyareVM()
                {
                    Id = teyyare.Id.ToString(),
                    Adi = teyyare.Adi ?? " ",
                    Tipi = teyyare.Tipi ?? " ",
                    Foto = Helpers.ConvertPhotoToBase64(teyyare.Foto),
                });
            }

            return View("Index", teyyarelerVM);
        }

        [HttpGet]
        public async Task<IActionResult> Etrafli(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            Teyyare teyyare = await _teyyareService.GetAsync(id);

            if (teyyare is null) return NotFound();

            ViewBag.Title = "Təyyarələr - " + teyyare.Adi;
            ViewBag.PageName = "teyyareler";

            GetTeyyareVM teyyareVM = new GetTeyyareVM()
            {
                Id = teyyare.Id.ToString(),
                Adi = teyyare.Adi,
                Tipi = teyyare.Tipi,
                IlkUcusTarixi = teyyare.IlkUcusTarixi,
                IstifadesiDavamEdirmi = teyyare.IstifadesiDavamEdirmi,

                Istehsalci = new IstehsalciVM()
                {
                    SirketinAdi = teyyare.Istehsalci.SirketinAdi,
                    Olke = teyyare.Istehsalci.Olke
                },
                Olculeri = new OlcuVM()
                {
                    Uzunlugu = teyyare.Olculeri.Uzunlugu,
                    Eni = teyyare.Olculeri.Eni,
                    Hundurluyu = teyyare.Olculeri.Hundurluyu,
                    Cekisi = teyyare.Olculeri.Cekisi
                },
                Qabiliyyetleri = new TeyyareQabiliyyetleriVM()
                {
                    MaksimumSureti = teyyare.Qabiliyyetleri.MaksimumSureti,
                    MaksimumUcusHundurluyu = teyyare.Qabiliyyetleri.MaksimumUcusHundurluyu
                },
                Qeyd = teyyare.Qeyd,
                Muherriki = new TeyyareMuherrikiVM()
                {
                    Modeli = teyyare.Muherriki.Modeli,
                    Istehsalcisi = teyyare.Muherriki.Istehsalcisi,
                    Gucu = teyyare.Muherriki.Gucu
                },
                Foto = Helpers.ConvertPhotoToBase64(teyyare.Foto),
            };

            return View(teyyareVM);
        }

        [HttpGet]
        public IActionResult ElaveEt()
        {
            ViewBag.Title = "Təyyarələr - Yeni təyyarə";
            ViewBag.FormTitle = "Yeni təyyarə əlavə et";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ElaveEt(AddTeyyareVM teyyareVM)
        {
            if (teyyareVM is null) View();

            teyyareVM.Foto = new byte[0];

            if (teyyareVM.FotoFile is not null && teyyareVM.FotoFile.Length > 0)
            {
                teyyareVM.Foto = Helpers.UploadPhoto(teyyareVM.FotoFile);
            }

            Teyyare teyyare = new Teyyare()
            {
                Adi = teyyareVM.Adi,
                Tipi = teyyareVM.Tipi,
                IlkUcusTarixi = teyyareVM.IlkUcusTarixi,
                IstifadesiDavamEdirmi = teyyareVM.IstifadesiDavamEdirmi,
                Istehsalci = new Istehsalci()
                {
                    SirketinAdi = teyyareVM.Istehsalci.SirketinAdi,
                    Olke = teyyareVM.Istehsalci.Olke
                },
                Olculeri = new Olcu()
                {
                    Uzunlugu = teyyareVM.Olculeri.Uzunlugu,
                    Eni = teyyareVM.Olculeri.Eni,
                    Hundurluyu = teyyareVM.Olculeri.Hundurluyu,
                    Cekisi = teyyareVM.Olculeri.Cekisi
                },
                Qabiliyyetleri = new TeyyareQabiliyyetleri()
                {
                    MaksimumSureti = teyyareVM.Qabiliyyetleri.MaksimumSureti,
                    MaksimumUcusHundurluyu = teyyareVM.Qabiliyyetleri.MaksimumUcusHundurluyu
                },
                Qeyd = teyyareVM.Qeyd,
                Muherriki = new TeyyareMuherriki()
                {
                    Modeli = teyyareVM.Muherriki.Modeli,
                    Istehsalcisi = teyyareVM.Muherriki.Istehsalcisi,
                    Gucu = teyyareVM.Muherriki.Gucu
                },
                Foto = teyyareVM.Foto
            };

            string yeniTeyyareId = await _teyyareService.CreateAsync(teyyare);
            return RedirectToAction(controllerName: "Teyyareler", actionName: "etrafli", routeValues: new { id = yeniTeyyareId });
        }

        [HttpGet]
        public async Task<IActionResult> DuzelisEt(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            Teyyare teyyare = await _teyyareService.GetAsync(id);

            if (teyyare is null) return NotFound();

            EditTeyyareVM teyyareVM = new EditTeyyareVM()
            {
                Id = id,
                Adi = teyyare.Adi,
                Tipi = teyyare.Tipi,
                IlkUcusTarixi = teyyare.IlkUcusTarixi,
                IstifadesiDavamEdirmi = teyyare.IstifadesiDavamEdirmi,

                Istehsalci = new IstehsalciVM()
                {
                    SirketinAdi = teyyare.Istehsalci.SirketinAdi,
                    Olke = teyyare.Istehsalci.Olke
                },
                Olculeri = new OlcuVM()
                {
                    Uzunlugu = teyyare.Olculeri.Uzunlugu,
                    Eni = teyyare.Olculeri.Eni,
                    Hundurluyu = teyyare.Olculeri.Hundurluyu,
                    Cekisi = teyyare.Olculeri.Cekisi
                },
                Qabiliyyetleri = new TeyyareQabiliyyetleriVM()
                {
                    MaksimumSureti = teyyare.Qabiliyyetleri.MaksimumSureti,
                    MaksimumUcusHundurluyu = teyyare.Qabiliyyetleri.MaksimumUcusHundurluyu
                },
                Qeyd = teyyare.Qeyd,
                Muherriki = new TeyyareMuherrikiVM()
                {
                    Modeli = teyyare.Muherriki.Modeli,
                    Istehsalcisi = teyyare.Muherriki.Istehsalcisi,
                    Gucu = teyyare.Muherriki.Gucu
                },
                Foto = teyyare.Foto,
                FotoDublikat = teyyare.Foto,
            };

            ViewBag.Title = teyyare.Adi + " - Düzəliş et";
            ViewBag.FormTitle = teyyare.Adi + " - Düzəliş et";

            return View(teyyareVM);
        }

        [HttpPost]
        public async Task<IActionResult> DuzelisEt(string id, EditTeyyareVM teyyareVM)
        {
            if (teyyareVM is null || string.IsNullOrEmpty(id)) return NotFound();

            if (teyyareVM.FotoFile is not null && teyyareVM.FotoFile.Length > 0)
            {
                teyyareVM.Foto = Helpers.UploadPhoto(teyyareVM.FotoFile);
            }else
            {
                teyyareVM.Foto = teyyareVM.FotoDublikat;
            }

            Teyyare teyyare = new Teyyare()
            {
                Adi = teyyareVM.Adi,
                Tipi = teyyareVM.Tipi,
                IlkUcusTarixi = teyyareVM.IlkUcusTarixi,
                IstifadesiDavamEdirmi = teyyareVM.IstifadesiDavamEdirmi,
                Istehsalci = new Istehsalci()
                {
                    SirketinAdi = teyyareVM.Istehsalci.SirketinAdi,
                    Olke = teyyareVM.Istehsalci.Olke
                },
                Olculeri = new Olcu()
                {
                    Uzunlugu = teyyareVM.Olculeri.Uzunlugu,
                    Eni = teyyareVM.Olculeri.Eni,
                    Hundurluyu = teyyareVM.Olculeri.Hundurluyu,
                    Cekisi = teyyareVM.Olculeri.Cekisi
                },
                Qabiliyyetleri = new TeyyareQabiliyyetleri()
                {
                    MaksimumSureti = teyyareVM.Qabiliyyetleri.MaksimumSureti,
                    MaksimumUcusHundurluyu = teyyareVM.Qabiliyyetleri.MaksimumUcusHundurluyu
                },
                Qeyd = teyyareVM.Qeyd,
                Muherriki = new TeyyareMuherriki()
                {
                    Modeli = teyyareVM.Muherriki.Modeli,
                    Istehsalcisi = teyyareVM.Muherriki.Istehsalcisi,
                    Gucu = teyyareVM.Muherriki.Gucu
                },
                Foto = teyyareVM.Foto
            };

            await _teyyareService.UpdateAsync(id, teyyare);

            return RedirectToAction(controllerName: "Teyyareler", actionName: "Etrafli", routeValues: new {id});
        }

        [HttpGet]
        public async Task<IActionResult> Sil(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            await _teyyareService.DeleteAsync(id);

            return RedirectToAction(controllerName: "Teyyareler", actionName: "Index"); 
        }
    }
}
