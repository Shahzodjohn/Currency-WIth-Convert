using Currency.Context;
using Currency.Models;
using Currency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace Currency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       [HttpPost]
        public async Task<IActionResult> Currency(string date)
        {
            using var client = new HttpClient();

            var result = await client.GetAsync($"https://nbt.tj/tj/kurs/export_xml.php?date={date}&export=xmlout");
            if (!result.IsSuccessStatusCode)
            {
                ModelState.AddModelError("Currency","Выберите дату");
                return View("index");
            }
            
            XmlSerializer formatter = new XmlSerializer(typeof(ValCurs));
            ValCurs valCurs = (ValCurs)formatter.Deserialize
                (new StringReader(await result.Content.ReadAsStringAsync()));

            //Для добавления всех валют в базу донных с сервера NBT


            //foreach(var item in valCurs.Valute)
            //{
            //    _context.Valutes.Add(new Valute 
            //    { 
            //        ID = item.ID,
            //        CharCode = item.CharCode,
            //        Name = item.Name,
            //        Nominal = item.Nominal,
            //        Value = item.Value,
            //    });
            //}
            //await _context.SaveChangesAsync();
            return View(valCurs);
        }
        [HttpGet]
        public IActionResult Converting() => View();
     
        [HttpPost]
        public IActionResult Converting(float amount, string charcode)
        {
            var res = _context.Valutes.FirstOrDefault(x => x.CharCode == charcode);
            if (res == null)
            {
                ModelState.AddModelError("Конвертация", "Ошибка конвертации, выберите валюту еще раз и введите сумму в текущей валюте!");
                return View("Converting");
            }
            
            var multiply = amount * float.Parse(res.Value.Replace(".",","));
            ViewBag.Message = multiply.ToString();
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
