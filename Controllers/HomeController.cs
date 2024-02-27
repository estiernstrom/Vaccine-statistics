
using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models;
using DSU24_Grupp5.Models.Dto;
using DSU24_Grupp5.Models.Dto.Batches;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.SCB;
using DSU24_Grupp5.Models.Dto.Sites;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using DSU24_Grupp5.Models.ViewModel;
using System.Security.Cryptography.X509Certificates;
using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.HeadModel;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace DSU24_Grupp5.Controllers
{
    public class HomeController : Controller
    {




        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {
            
             IndexFilterViewModel model = new IndexFilterViewModel();
            return View(model);           
        }


        [HttpPost]
        public async Task<IActionResult> Index(IndexFilterViewModel model)
        {            
            return View(model);
        }

        [HttpPost]
        public List<string> GetBachNumberByVaccineName(string selectedVaccineName)
        {
            List<string> batchNameList = new List<string>();
            batchNameList = StatisticsHelper.GetBatchNumberByVaccineName(selectedVaccineName);

            return batchNameList;
        }

        [HttpPost] 
        public List<string> GetVaccineNameByManufacturer(string selectedManufacturer)
        {
            List<string> vaccineNameList = new List<string>();
            vaccineNameList = StatisticsHelper.GetVaccineNameByManufacturer(selectedManufacturer);

            return vaccineNameList;
        }





        [HttpPost]
        public List<string> GetManufacturerByVaccineName(string selectedVaccine)
        {
            List<string> manufacturerList = new List<string>();
            manufacturerList = StatisticsHelper.GetManufacturerByVaccineName(selectedVaccine);
            return manufacturerList;
        }

        public IActionResult Map()
        {
            return View();
        }

        public IActionResult Compare()
        {
            IndexFilterViewModel model = new IndexFilterViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Compare(IndexFilterViewModel indexFilterViewModel) 
        { 
            return View(indexFilterViewModel);
        }


        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
