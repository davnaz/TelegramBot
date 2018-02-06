using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Components;

namespace TelegramBot
{
    class Program
    {
        private static TelegramBotClient bot = new TelegramBotClient(Resources.ApiKey); //инициализация бота
        static ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(); //заготовка для будущих клавиатур для ответа
        static void Main(string[] args)
        {
            bot.StartReceiving(); 
            bot.OnMessage += BotOnMessage; //вешаем обработчик
            //bot.SendTextMessageAsync(Resources.AdminUserID, String.Format("Бот заработал на {0}, IP: {1}.", System.Environment.MachineName,Environment.OSVersion));
            Console.ReadLine();
        }

        private static async void BotOnMessage(object sender, MessageEventArgs e)
        {
            if(e.Message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage) //если нам прислали текстовое сообщение, то 
            {
                string message = e.Message.Text;  //берем сообщение
                long chatID = e.Message.Chat.Id; 
                string name = e.Message.From.FirstName + " " + e.Message.From.LastName;
                int userId = e.Message.From.Id; 
                string userName = e.Message.From.Username;
                Console.WriteLine("{0}(@{1}) - {2}: {3}",userId,userName,name,message);
                String Answer = "";


                switch (message)
                {
                    case "/start": Answer = "Этот бот помогает определить цену ноутбуков определенных моделей."; break;
                    case "/lenovoy720": Answer = Parser.getPrice(1); break;  //выполняет запрос в БД
                    case "/dell": Answer = Parser.getPrice(2); break;
                    case "/asusrog1": Answer = Parser.getPrice(3); break;
                    case "/asusrog2": Answer = Parser.getPrice(4); break;
                    case "/asusrog3": Answer = Parser.getPrice(5); break;
                    case "/asusrog4": Answer = Parser.getPrice(6); break;
                    case "/asusrog5": Answer = Parser.getPrice(7); break;
                    case "/asusrog6": Answer = Parser.getPrice(8); break;
                    default: Answer = "Неправильный запрос."; break;
                }
                await bot.SendTextMessageAsync(userId, Answer);
            }
            
        }
    }
}
