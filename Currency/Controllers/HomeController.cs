using Currency.Context;
using Currency.Models;
using Currency.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        public IActionResult HomePage() => View();
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

       [HttpPost]
       [Authorize]
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
            #region
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
            #endregion


            return View(valCurs);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Converting() => View();
     
        [HttpPost]
        public IActionResult Converting(float amount, string charcode)
        {
            var charCode = _context.Valutes.FirstOrDefault(x => x.CharCode == charcode);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Конвертация", "Ошибка конвертации, выберите валюту еще раз и введите сумму в текущей валюте!");
                return View("Converting");
            }
            
            var multiply = amount * float.Parse(charCode.Value.Replace(".",","));
            ViewBag.Amount = amount;
            ViewBag.Char = charcode;
            ViewBag.Mul = multiply.ToString();
            return View();

        }
        [Authorize]
        [HttpGet]
        public IActionResult Deposit() => View();

        [HttpPost]
        public async Task<IActionResult> Deposit(float amount, string charcode)
        {
            var CharCode = await _context.Valutes.FirstOrDefaultAsync(x => x.CharCode == charcode);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Депозит", "Ошибка! Выберите валюту и введите сумму депозита.");
                return View("Deposid");
            }
            var PercentsSomoni = (amount * 17 / 100)+ amount;
            var PercentsUsd = (amount * 6 / 100) + amount;
            if(charcode == "USD")
            {
                ViewBag.Result = PercentsUsd;
            }
            if (charcode == "TJS")
            {
                ViewBag.Result = PercentsSomoni;
            }
            ViewBag.CharCode = charcode;
            ViewBag.Amount = amount;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
