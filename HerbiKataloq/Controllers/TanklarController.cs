using HerbiKataloq.Common;
using HerbiKataloq.Services.TankServices;
using Microsoft.AspNetCore.Mvc;
using HerbiKataloq.Models.TankModels;
using HerbiKataloq.ViewModels.Tank;
using HerbiKataloq.ViewModels;
using HerbiKataloq.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using HerbiKataloq.Models.TeyyareModels;
using HerbiKataloq.ViewModels.Teyyare;

namespace HerbiKataloq.Controllers
{
    public class TanklarController : Controller
    {
        private readonly ITankService _tankService;

        public TanklarController(ITankService tankService)
        {
            _tankService = tankService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string category = null)
        {
            List<IndexTankVM> tanklarVM = [];

            if (category is not null)
            {
                ViewBag.Title = "Tanklar - " + category;
                ViewBag.PageName = "tanklar";

                List<Tank> tanklarByCategory = await _tankService.GetAllByCategory(category);

                foreach (Tank tank in tanklarByCategory)
                {
                    tanklarVM.Add(new IndexTankVM()
                    {
                        Id = tank.Id.ToString(),
                        Adi = tank.Adi ?? " ",
                        Sinfi = tank.Sinfi ?? " ",
                        Foto = Helpers.ConvertPhotoToBase64(tank.Foto),
                    });
                }

                return View(tanklarVM);
            }

            ViewBag.Title = "Tanklar";
            ViewBag.PageName = "tanklar";

            List<Tank> tanklar = await _tankService.GetAllAsync();


            foreach (Tank tank in tanklar)
            {
                tanklarVM.Add(new IndexTankVM()
                {
                    Id = tank.Id.ToString(),
                    Adi = tank.Adi ?? " ",
                    Sinfi = tank.Sinfi ?? " ",
                    Foto = Helpers.ConvertPhotoToBase64(tank.Foto),
                });
            }

            return View(tanklarVM);
        }

        [HttpGet]
        public async Task<IActionResult> Axtar(string keyOfTank, string valueOfTank)
        {
            if (string.IsNullOrEmpty(keyOfTank) || string.IsNullOrEmpty(valueOfTank)) return NotFound();

            List<Tank> tanklar = await _tankService.Find(keyOfTank, valueOfTank);

            if (tanklar is null && tanklar.Count == 0) return NotFound();

            List<IndexTankVM> tanklarVM = [];

            foreach (Tank tank in tanklar)
            {
                tanklarVM.Add(new IndexTankVM()
                {
                    Id = tank.Id.ToString(),
                    Adi = tank.Adi ?? " ",
                    Sinfi = tank.Sinfi ?? " ",
                    Foto = Helpers.ConvertPhotoToBase64(tank.Foto),
                });
            }

            return View("Index", tanklarVM);
        }

        [HttpGet]
        public async Task<IActionResult> Etrafli(string id)
        {
            if(string.IsNullOrEmpty(id)) return NotFound();

            Tank tank = await _tankService.GetAsync(id);

            if (tank is null) return NotFound();

            ViewBag.Title = "Tanklar - " + tank.Adi;
            ViewBag.PageName = "tanklar";

            GetTankVM tankVM = new GetTankVM()
            {
                Id = tank.Id.ToString(),
                Adi = tank.Adi,
                Sinfi = tank.Sinfi,
                Zirehi = tank.Zirehi,
                YanacaqTutumu = tank.YanacaqTutumu,
                PersonalSayi = tank.PersonalSayi,
                Istehsalci = new IstehsalciVM()
                {
                    SirketinAdi = tank.Istehsalci.SirketinAdi,
                    Olke = tank.Istehsalci.Olke
                },
                Olculeri = new OlcuVM()
                {
                    Uzunlugu = tank.Olculeri.Uzunlugu,
                    Eni = tank.Olculeri.Eni,
                    Hundurluyu = tank.Olculeri.Hundurluyu,
                    Cekisi = tank.Olculeri.Cekisi
                },
                Qabiliyyetleri = new TankQabiliyyetleriVM()
                {
                    MuharibeRadiusu = tank.Qabiliyyetleri.MuharibeRadiusu,
                    YoldaMaksimumSureti = tank.Qabiliyyetleri.YoldaMaksimumSureti,
                    YolsuzluqSeraitindeMaksimumSureti = tank.Qabiliyyetleri.YolsuzluqSeraitindeMaksimumSureti
                },
                Muherriki = new TankMuherrikiVM()
                {
                    Modeli = tank.Muherriki.Modeli,
                    Istehsalcisi = tank.Muherriki.Istehsalcisi,
                    YanacaqNovu = tank.Muherriki.YanacaqNovu,
                    Gucu = tank.Muherriki.Gucu
                },
                Foto = Helpers.ConvertPhotoToBase64(tank.Foto),
                Qeyd = tank.Qeyd,
            };

            if(tank.Silahlari is not null)
            {
                tankVM.Silahlari = new List<TankSilahiVM>();

                foreach (TankSilahi silah in tank.Silahlari)
                {
                    tankVM.Silahlari.Add(new TankSilahiVM()
                    {
                        Adi = silah.Adi,
                        Teyinati = silah.Teyinati,
                        Capi = silah.Capi,
                        AtisMesafesi = silah.AtisMesafesi
                    });
                }
            }

            return View(tankVM);
        }

        [HttpGet]
        public IActionResult ElaveEt()
        {
            ViewBag.Title = "Tanklar - Yeni tank";
            ViewBag.FormTitle = "Yeni tank əlavə et";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ElaveEt(AddTankVM tankVM)
        {
            if (tankVM is null) return View();

            tankVM.Foto = new byte[0];

            if (tankVM.FotoFile is not null && tankVM.FotoFile.Length > 0)
            {
                tankVM.Foto = Helpers.UploadPhoto(tankVM.FotoFile);
            }

            Tank tank = new Tank()
            {
                Adi = tankVM.Adi,
                Sinfi = tankVM.Sinfi,
                Zirehi = tankVM.Zirehi,
                YanacaqTutumu = tankVM.YanacaqTutumu,
                PersonalSayi = tankVM.PersonalSayi,
                Istehsalci = new Istehsalci()
                {
                    SirketinAdi = tankVM.Istehsalci.SirketinAdi,
                    Olke = tankVM.Istehsalci.Olke
                },
                Olculeri = new Olcu()
                {
                    Uzunlugu = tankVM.Olculeri.Uzunlugu,
                    Eni = tankVM.Olculeri.Eni,
                    Hundurluyu = tankVM.Olculeri.Hundurluyu,
                    Cekisi = tankVM.Olculeri.Cekisi
                },
                Qabiliyyetleri = new TankQabiliyyetleri()
                {
                    MuharibeRadiusu = tankVM.Qabiliyyetleri.MuharibeRadiusu,
                    YoldaMaksimumSureti = tankVM.Qabiliyyetleri.YoldaMaksimumSureti,
                    YolsuzluqSeraitindeMaksimumSureti = tankVM.Qabiliyyetleri.YolsuzluqSeraitindeMaksimumSureti
                },
                Muherriki = new TankMuherriki()
                {
                    Modeli = tankVM.Muherriki.Modeli,
                    Istehsalcisi = tankVM.Muherriki.Istehsalcisi,
                    YanacaqNovu = tankVM.Muherriki.YanacaqNovu,
                    Gucu = tankVM.Muherriki.Gucu
                },
                Foto = tankVM.Foto,
                Qeyd = tankVM.Qeyd,
            };

            string yeniTankId = await _tankService.CreateAsync(tank);
            return RedirectToAction(controllerName: "Tanklar", actionName: "Etrafli", routeValues: new { id = yeniTankId });
        }

        [HttpGet]
        public async Task<IActionResult> DuzelisEt(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            Tank tank = await _tankService.GetAsync(id);

            if (tank is null) return NotFound();

            EditTankVM tankVM = new EditTankVM()
            {
                Id = tank.Id,
                Adi = tank.Adi,
                Sinfi = tank.Sinfi,
                Zirehi = tank.Zirehi,
                YanacaqTutumu = tank.YanacaqTutumu,
                PersonalSayi = tank.PersonalSayi,
                Istehsalci = new IstehsalciVM()
                {
                    SirketinAdi = tank.Istehsalci.SirketinAdi,
                    Olke = tank.Istehsalci.Olke
                },
                Olculeri = new OlcuVM()
                {
                    Uzunlugu = tank.Olculeri.Uzunlugu,
                    Eni = tank.Olculeri.Eni,
                    Hundurluyu = tank.Olculeri.Hundurluyu,
                    Cekisi = tank.Olculeri.Cekisi
                },
                Qabiliyyetleri = new TankQabiliyyetleriVM()
                {
                    MuharibeRadiusu = tank.Qabiliyyetleri.MuharibeRadiusu,
                    YoldaMaksimumSureti = tank.Qabiliyyetleri.YoldaMaksimumSureti,
                    YolsuzluqSeraitindeMaksimumSureti = tank.Qabiliyyetleri.YolsuzluqSeraitindeMaksimumSureti
                },
                Muherriki = new TankMuherrikiVM()
                {
                    Modeli = tank.Muherriki.Modeli,
                    Istehsalcisi = tank.Muherriki.Istehsalcisi,
                    YanacaqNovu = tank.Muherriki.YanacaqNovu,
                    Gucu = tank.Muherriki.Gucu
                },
                Foto = tank.Foto,
                FotoDublikat = tank.Foto,
                Qeyd = tank.Qeyd,
            };

            ViewBag.Title = tank.Adi + " - Düzəliş et";
            ViewBag.FormTitle = tank.Adi + " - Düzəliş et";

            return View(tankVM);
        }

        [HttpPost]
        public async Task<IActionResult> DuzelisEt(string id, EditTankVM tankVM)
        {
            if (tankVM is null || string.IsNullOrEmpty(id)) return NotFound();

            if (tankVM.FotoFile is not null && tankVM.FotoFile.Length > 0)
            {
                tankVM.Foto = Helpers.UploadPhoto(tankVM.FotoFile);
            }
            else
            {
                tankVM.Foto = tankVM.FotoDublikat;
            }

            Tank tank = new Tank()
            {
                Adi = tankVM.Adi,
                Sinfi = tankVM.Sinfi,
                Zirehi = tankVM.Zirehi,
                YanacaqTutumu = tankVM.YanacaqTutumu,
                PersonalSayi = tankVM.PersonalSayi,
                Istehsalci = new Istehsalci()
                {
                    SirketinAdi = tankVM.Istehsalci.SirketinAdi,
                    Olke = tankVM.Istehsalci.Olke
                },
                Olculeri = new Olcu()
                {
                    Uzunlugu = tankVM.Olculeri.Uzunlugu,
                    Eni = tankVM.Olculeri.Eni,
                    Hundurluyu = tankVM.Olculeri.Hundurluyu,
                    Cekisi = tankVM.Olculeri.Cekisi
                },
                Qabiliyyetleri = new TankQabiliyyetleri()
                {
                    MuharibeRadiusu = tankVM.Qabiliyyetleri.MuharibeRadiusu,
                    YoldaMaksimumSureti = tankVM.Qabiliyyetleri.YoldaMaksimumSureti,
                    YolsuzluqSeraitindeMaksimumSureti = tankVM.Qabiliyyetleri.YolsuzluqSeraitindeMaksimumSureti
                },
                Muherriki = new TankMuherriki()
                {
                    Modeli = tankVM.Muherriki.Modeli,
                    Istehsalcisi = tankVM.Muherriki.Istehsalcisi,
                    YanacaqNovu = tankVM.Muherriki.YanacaqNovu,
                    Gucu = tankVM.Muherriki.Gucu
                },
                Foto = tankVM.Foto,
                Qeyd = tankVM.Qeyd,
            };

            await _tankService.UpdateAsync(id, tank);

            return RedirectToAction(controllerName: "Tanklar", actionName: "Etrafli", routeValues: new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Sil(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            await _tankService.DeleteAsync(id);

            return RedirectToAction(controllerName: "Tanklar", actionName: "Index");
        }

        [HttpGet]
        public IActionResult SilahElaveEt(string id)
        {
            if(string.IsNullOrEmpty(id)) return NotFound();

            TankSilahiVM tankSilahiVM = new TankSilahiVM();
            tankSilahiVM.TankId = id;

            ViewBag.Title = " Silah əlavə et";

            return View(tankSilahiVM);
        }

        [HttpPost]
        public async Task<IActionResult> SilahElaveEt(string id, TankSilahiVM tankSilahiVM)
        {
            if(string.IsNullOrEmpty(id) || tankSilahiVM is null) return NotFound();

            TankSilahi tankSilahi = new TankSilahi()
            {
                Adi = tankSilahiVM.Adi,
                Teyinati = tankSilahiVM.Teyinati,
                Capi = tankSilahiVM.Capi,
                AtisMesafesi = tankSilahiVM.AtisMesafesi
            };

            await _tankService.AddWeapon(id, tankSilahi);

            return RedirectToAction(controllerName: "Tanklar", actionName: "Etrafli", routeValues: new { id });
        }
    }
}
