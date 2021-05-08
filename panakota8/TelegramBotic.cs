using System;
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

namespace panakota8
{
    internal class TelegramBotic
    {
        private const string _laws = "законопроекты";
        private const string support = "за";
        private const string against = "против";
        private const string absent = "отсутствовал";
        private const string proponent = "инициатор";
        private const string all = "все";
        private const string _declaration = "Налоговая декларация";
        private const string back = "назад";

        private string detuty = "";

        processingDatasFormApi rec = new processingDatasFormApi();

        string[] statuses = { "отсутствовал", "за", "против", "инициатор"};
        Decision[] decisions = { Decision.Absent, Decision.Agreed, Decision.Rejected, Decision.Initiator };



        private string _token;
        Telegram.Bot.TelegramBotClient _client;


        public TelegramBotic(string token)
        {
            this._token = token;
        }

        internal void GetUppdates()
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
                        if(updates != null && updates.Count() > 0)
                        {
                            foreach(var update in updates)
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
            switch(update.Type)
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
                        case "/start":
                            _client.SendTextMessageAsync(update.Message.Chat.Id, @"Привет, вы можете узнать: 
    1)результаты взаимодействия депутата с законопроектами рассмотренными в ЖК
    2)декларацию депутата

Прошу если вас осенит и у вас появятся мысли на счет функционала бота или сайта дайте нам знать, ждем все глупые(и не только) идейки. Также можете, либо лично написать любому члену команды проекта panakota, либо в любую из общих YLSP групп, будем рады прочитать любую откровенность.
Ждем от вас любой совет или предложение, любую критику, каждое субъективное мнение


Вам нужно просто ввести ФИО депутата шестого созыва в укороченном виде

п.с. регистр можно не соблюдать, но последовательность - надо(например: БекеШев Д.Д.");
                            break;
                        case _laws:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "голосовал(-а)", replyMarkup: GetButtonsOfStatuses());
                            break;
                        case all:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, $"{statuses[0]}:\n{rec.TheDecision(detuty, decisions[0])}\n" +
                                $"{statuses[1]}:\n{rec.TheDecision(detuty, decisions[1])}\n" +
                                $"{statuses[2]}:\n{rec.TheDecision(detuty, decisions[2])}\n" +
                                $"{statuses[3]}:\n{rec.TheDecision(detuty, decisions[3])}\n");
                            break;
                        case _declaration:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Налоговая декларация еще в разработке", replyMarkup: GetButtonsOfDeclaration());
                            break;
                        case back:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "законопроекты с которыми взаимодействовал(-а) " + text + " или ее(его) декларация", replyMarkup: GetButtons());
                            break;
                    }


                    for (int j = 0; j < statuses.Length; j++)
                    {
                        if(text == statuses[j])
                        {
                            _client.SendTextMessageAsync(update.Message.Chat.Id, $"{statuses[j]}:\n{rec.TheDecision(detuty, decisions[j])}");
                        }
                    }
                    break;
                default:
                    Console.WriteLine($"{update.Type} not implemented" );
                    break;
            }
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


        private IReplyMarkup GetButtonsOfDeclaration()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                        new List<KeyboardButton>{new KeyboardButton { Text = back}}
                }
            };
        }


        private IReplyMarkup GetButtonsOfStatuses()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                        new List<KeyboardButton>{new KeyboardButton { Text = support }, new KeyboardButton { Text = against } },
                        new List<KeyboardButton>{new KeyboardButton { Text = absent}, new KeyboardButton { Text = proponent} },
                        new List<KeyboardButton>{new KeyboardButton { Text = all}},
                        new List<KeyboardButton>{new KeyboardButton { Text = back}}
                }
            };
        }
    }
}