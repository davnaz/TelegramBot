using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System.Data.SqlClient;
using System.IO;
using System.Net;


namespace TelegramBot.Components
{
    class Parser 
    {
        private static WebProxy currentProxy;
        private static HtmlParser parser;

        public Parser()
        {
            parser = new HtmlParser(); //создание экземпляра парсера, он можнт быть использован несколько раз для одного потока(экземпляра класса Parser)
        } 
        

        private Notebook ParseOffer(IHtmlDocument htmlDocument) //парсит документ DOM конкретного предложения жилья
        {
            
            if (htmlDocument.QuerySelector("link") == null)
            {
                Console.WriteLine("Ссылка битая!");
                Console.WriteLine("Прокси обновлен:" + currentProxy.Address);
                return null;
            }
            else
            {
                Console.WriteLine("Предложение {0} спарсили!", htmlDocument.QuerySelector("link").GetAttribute("href"));
                return null;
            }
        }

        public static string getPrice(int Id)
        {
            string query = "SELECT * FROM dbo.Items WHERE Id = " + Id.ToString();  //стучится в БД и запрашивает данные
            Notebook n = new Notebook(DataProviders.DataProvider.Instance.GetDataRowFromDb(query));  //формирует экземпляр
            string pageInline = WebHelpers.GetHtml(n.Link);  //добываем HTML страницы сайта
            if (pageInline == Constants.WebAttrsNames.NotFound) //если страница не найдена, тогда все =(
            {
                return "Страница не найдена!";
            }
            HtmlParser p = new HtmlParser();
            IHtmlDocument document = p.Parse(pageInline); //запарсили страницу в DOM
            string price = document.QuerySelector(".inlineb").TextContent; //получили цену
            return n.Name + " стоит " + price + " рублей. "+n.Link; 
        }

    }
}