using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using panakota8.processing;
using panakota8.Tripl_Trap;
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
                        _client.SendTextMessageAsync(update.Message.Chat.Id, "законопроекты с которыми взаимодействовал(-а) " + text + " или ее(его) декларация", replyMarkup: GetButtons());
                    }


                    switch(text)
                    {
                        case _laws:
                            //var laws = GetListOfInlineButtons();
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "законопроекты за которые голосовал " + detuty + " ЗА ", replyMarkup: GetInlineButtons(2));
                            break;
                    }

                    break;
            }
        }

        private IReplyMarkup GetInlineButtons(int id)
        {
            var gett = GetListOfInlineButtons("Бекешев Д.Д.", Decision.Agreed);
            var inlineKeyboard = new InlineKeyboardMarkup(Gets(gett));

            return inlineKeyboard;
        }

        private List<InlineKeyboardButton> GetListOfInlineButtons(string detuty, Decision status)
        {
            TriplProcessingDatasFromAPI trip = new TriplProcessingDatasFromAPI();
            List<string> LawsForTheDecision = trip.TheDecision(detuty, status).GetRange(0,42);
            
            List<InlineKeyboardButton> ButtonsForLaws = new List<InlineKeyboardButton>();
            for (int i = 0; i < LawsForTheDecision.Count; i++)
            {
                ButtonsForLaws.Add(InlineKeyboardButton.WithCallbackData($"{LawsForTheDecision[i]}", $"return {i}"));
            }
            return ButtonsForLaws;
        }
        private InlineKeyboardButton[][] Gets(List<InlineKeyboardButton> setButtons)
        {
            int len = setButtons.Count;
            InlineKeyboardButton[][] inlineKeyboards = new InlineKeyboardButton[len][];

            int i = 0;
            while (i < len )
            {
                InlineKeyboardButton[] HelperList = setButtons.GetRange(setButtons.Count - 1, 1).ToArray();
                setButtons.RemoveRange(setButtons.Count - 1, 1);

                inlineKeyboards[i] = HelperList;
                i++;
            }

            return inlineKeyboards;
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