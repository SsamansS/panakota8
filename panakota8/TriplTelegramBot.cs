using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using panakota8.processing;
using Telegram.Bot.Types.ReplyMarkups;

namespace panakota8
{
    internal class TriplTelegramBot
    {
        private const string _laws = "законопроекты";
        private const string _declaration = "Налоговая декларация";


        private string _token;
        Telegram.Bot.TelegramBotClient _client;


        public TriplTelegramBot(string token)
        {
            this._token = token;
        }

        processingDatasFormApi rec = new processingDatasFormApi();
        private string detuty = "";

        internal void TriplGetUppdates()
        {
            _client = new Telegram.Bot.TelegramBotClient(_token);
            var me = _client.GetMeAsync().Result;
            if (me != null && !string.IsNullOrEmpty(me.Username))
            {
                int offset = 0;
                while (true)
                {
                    try
                    {
                        var updates = _client.GetUpdatesAsync(offset).Result;
                        if (updates != null && updates.Count() > 0)
                        {
                            foreach (var update in updates)
                            {
                                processUpdate(update);
                                offset = update.Id + 1;
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }

                    Thread.Sleep(1000);

                }
            }
        }

        private void processUpdate(Telegram.Bot.Types.Update update)
        {

            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    var text = update.Message.Text;
                    Console.WriteLine($"{ update.Message.From.FirstName} отправил '{text}'");

                    if (rec.CheckDeputy(text) != "f")
                    {
                        detuty = rec.CheckDeputy(text);
                        _client.SendTextMessageAsync(update.Message.Chat.Id, "законопроекты с которыми взаимодействовал(-а) " + text + " или ее(его) декларация", replyMarkup: GetInlineButtons(update.Id));
                    }

                    break;
            }
        }

        private IReplyMarkup GetInlineButtons(int id)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
               {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1.1", "11"),
                        InlineKeyboardButton.WithCallbackData("1.2", "12"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("2.1", "21"),
                        InlineKeyboardButton.WithCallbackData("2.2", "22"),
                    }
                });

            return inlineKeyboard;

            //return new InlineKeyboardMarkup(new[]
            //{
                
                
                /*InlineKeybod = new List<List<InlineKeyboardButton>>
                {
                    new List<InlineKeyboardButton>{ new InlineKeyboardButton { Text = _laws }, new InlineKeyboardButton { Text = _declaration} },
                    new List<InlineKeyboardButton>{ new InlineKeyboardButton { Text = _laws }, new InlineKeyboardButton { Text = _declaration} }
                }*/
                
                /*new InlineKeyboardButton { Text = "Заказукккуцкуцкуцкькуккккккккккккккккккккккккккккккк                         каку                           ать", CallbackData = id.ToString() },
                new InlineKeyboardButton { Text = "Заь", CallbackData = id.ToString() },
                new InlineKeyboardButton { Text = "Закываваыазать", CallbackData = id.ToString() },
                new InlineKeyboardButton { Text = "Заказукккуцкуцкуцкькуккккккккккккккккккккккккккккккк                         каку                           ать", CallbackData = id.ToString() },
                new InlineKeyboardButton { Text = "Заь", CallbackData = id.ToString() },
                new InlineKeyboardButton { Text = "Закываваыазать", CallbackData = id.ToString() }*/
            //});

            //return new InlineKeyboardMarkup (new InlineKeyboardButton { Text = "Заказать", CallbackData = id.ToString() } );
            /*{
                InlineKeyboard = new InlineKeyboardButton
                {
                    Text = " заказать", CallbackData = id.ToString()
                }
            };*/
        }

        private IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = _laws }, new KeyboardButton { Text = _declaration} }
                }
            };
        }


    }
}