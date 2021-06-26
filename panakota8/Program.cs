using System;
using Telegram.Bot;

using System.Linq;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;

using System.IO;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft;
using Newtonsoft.Json;

using panakota8.processing;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;

namespace panakota8
{
    class Program
    {
        static TelegramBotClient Bot; //создали объект
        static void Main(string[] args)
        {
            try
            {
                TelegramBotic hlp = new TelegramBotic(token: "1807528983:AAHpWxKKkDDYRTtNGD9rXBFKh8no3Hrtkw4");
                hlp.GetUppdates();
            }

            catch (Exception ex) { Console.WriteLine(ex.Message); }

            //try
            //{
            //    Bot = new TelegramBotClient("1807528983:AAHpWxKKkDDYRTtNGD9rXBFKh8no3Hrtkw4"); //ввели код от БотФазер

            //    Bot.OnMessage += Bot_OnMessageReceived; //события, чтобы получить смс, которое вел пользователь бота
            //    Bot.OnCallbackQuery += Bot_OnCallbackQueryReceived;

            //    var me = Bot.GetMeAsync().Result;//инфа про бота(имя, ник и тд)

            //    Console.WriteLine(me.FirstName);

            //    Bot.StartReceiving(); //чтобы начать получать смски пользователей(в тг которые пишут)
            //    Console.ReadLine();
            //    Bot.StopReceiving();//чтобы остановить смски пользователей для обработки
            //}

            //catch (Exception ex) { Console.WriteLine(ex.Message); }

            //processingDatasFormApi rec = new processingDatasFormApi();
            //List<string> TheDecisionLikeList = rec.TheDecisionLikeList("Бекешев Д.Д.", Decision.Rejected);

            //Console.WriteLine(TheDecisionLikeList.Count);
        }
        private static async void Bot_OnCallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName } {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} нажал на кнопку {buttonText}");

            await Bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"вы нажали на кнопку{buttonText}");
        }

        private static async void Bot_OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e) //появилось когда создали Bot.OnMessage
        {
            var message = e.Message;// смс от пользователя

            if (message == null || message.Type != MessageType.Text)
                return;

            Console.WriteLine($"{message.Text} от {message.From.FirstName}");

            switch (message.Text)
            {
                case "/start":
                    string text =
@"Привет, я бот ебобот
отправь имя депутата";
                    await /*чтобы обратывалось смс от неск-х юзеров*/ Bot.SendTextMessageAsync(message.From.Id/* находим отправителя по айПи*/, text /*отправляем текс*/);
                    break;
                case "Дастан Бекешев":
                    await Bot.SendTextMessageAsync(message.From.Id, @"голосовал против: 
1)Законопроект о манипулировании информацией в интернете
2)Законопроект о приостановлении действия некоторых норм конституционного закона «О выборах Президента КР и депутатов Жогорку Кенеша КР»

Инициатор:
1)Избирательный залог и избирательный порог в ходе выборов депутатов Жогорку Кенеша Кыргызской Республики
2)Совершенствование законодательства – помощь молодым специалистам
3)Мораторий на разработку и добычу урана на биосферной территории «Ыссык-Кёль»
4)Узаконить онлайн-петиции
");
                    break;
                case "Каныбек Иманалиев":
                    await Bot.SendTextMessageAsync(message.From.Id, @"Инициатор
1)Поправки по снижению избирательного барьера на парламентских выборах с 9 до 7% ");
                    break;
                case "":
                    await Bot.SendTextMessageAsync(message.From.Id, @"Инициатор
1)пустоты");
                    break;
                default:
                    await Bot.SendTextMessageAsync(message.From.Id, "введите Иф депутата");
                    break;
            }
        }
    }
}
