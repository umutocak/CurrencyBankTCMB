using CurrencyBankTCMB.models;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace CurrencyBankTCMB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {

        [HttpGet("get-currency")]
        public IActionResult GetCurrency()
        {
            XmlDocument doc1 = new XmlDocument();
            doc1.Load("https://www.tcmb.gov.tr/kurlar/today.xml");
            XmlElement root = doc1.DocumentElement!;
            XmlNodeList nodes = root.SelectNodes("/Tarih_Date/Currency")!;
            List<Currency> currencies = new List<Currency>();
            foreach (XmlNode node in nodes)
            {
                Currency currency = new Currency();
                currency.Code = node.Attributes!["Kod"]!.Value;
                currency.ForexBuying = node["ForexBuying"]!.InnerText;
                currency.ForexSelling = node["ForexSelling"]!.InnerText;
                currency.BanknoteBuying = node["BanknoteBuying"]!.InnerText;
                currency.BanknoteSelling = node["BanknoteSelling"]!.InnerText;
                currencies.Add(currency);
            }
            return currencies != null ? Ok(currencies) : BadRequest(currencies);
        }
    }
}
