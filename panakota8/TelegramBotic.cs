using System;
using System.Linq;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;

namespace panakota8
{
    internal class TelegramBotic
    {
        private const string _laws = "законопроекты";
        private const string _declaration = "декларация";
        private const string support = "за";
        private const string against = "против";
        private const string absent = "не был";
        private const string proponent = "инициатор";
        private const string all = "все";
        private string detuty = "";

        private const string _oneDateOfDiclaration = "на 05.04.2015";
        private const string _twoDateOfDiclaration = "на 06.04.2020";


        List<string> depities = new List<string>
            {
                "токтосунова айназик",
                "цой данил",
                "конушбекова калия"
            };
        List<string> ListLaws = new List<string>
            {
                "закон сохранения энергии",
                "закон сохранения импульса",
                "закон Ома",
                "закон об принятии реализма",
                "закон об включении мозгов",
                "закон об уничтожении человечества"
            };
        string[] statuses = { "за", "против", "не был", "инициатор" };

        List<string> forDeclaration = new List<string>
            {
                $"токтосунова айназик {_oneDateOfDiclaration}",
                $"токтосунова айназик {_twoDateOfDiclaration}",
                $"цой данил {_oneDateOfDiclaration}",
                $"цой данил {_twoDateOfDiclaration}",
                $"конушбекова калия {_oneDateOfDiclaration}",
                $"конушбекова калия {_twoDateOfDiclaration}"
            };


        static Dictionary<string, string> Declarations = new Dictionary<string, string>()
        {
            { $"токтосунова айназик {_oneDateOfDiclaration}", "1)чупачупс\n2)iphone 5"},
            { $"токтосунова айназик {_twoDateOfDiclaration}", "1)4х квартира в 'Avangard' рядом с 'Глобус'\n2)Tesla Roadster\n3)$1000000 за решение гипотезы Римана\n4)чупачупс\n5)iphone 5"},
            { $"цой данил {_oneDateOfDiclaration}", "1)100 листа А4;\n2)однакомнатная квартира(41 кв м)"},
            { $"цой данил {_twoDateOfDiclaration}", "1)100 сертификатов\n2)восьмикомнатная квартира(41 кв м)"},
            { $"конушбекова калия {_oneDateOfDiclaration}", "1)$100000 с продажи YouTube-канала \"успехный успеш Брокера\"\n2)зарядка от ипхоне\n3)майнинг ферма в Техасе"},
            { $"конушбекова калия {_twoDateOfDiclaration}", "1)зарядка от ипхоне"}
        };



        static Dictionary<string, string> закон_сохранения_энергии = new Dictionary<string, string>()
            {
                { "за", "токтосунова айназик, цой данил"},
                {"против", "конушбекова калия" },
                {"не был", "" },
                {"инициатор", "" }
            };

        static Dictionary<string, string> закон_сохранения_импульса = new Dictionary<string, string>()
            {
                { "за", ""},
                {"против", "цой данил" },
                {"не был", "токтосунова айназик, конушбекова калия" },
                {"инициатор", "конушбекова калия" }
            };

        static Dictionary<string, string> закон_Ома = new Dictionary<string, string>()
            {
                { "за", "цой данил, конушбекова калия"},
                {"против", "токтосунова айназик" },
                {"не был", "" },
                {"инициатор", "конушбекова калия" }
            };

        static Dictionary<string, string> закон_о_принятии_реализма = new Dictionary<string, string>()
            {
                { "за", "токтосунова айназик, конушбекова калия"},
                {"против", "" },
                {"не был", "цой данил" },
                {"инициатор", "цой данил" }
            };

        static Dictionary<string, string> закон_о_включении_мозгов = new Dictionary<string, string>()
            {
                { "за", "токтосунова айназик"},
                {"против", "" },
                {"не был", "конушбекова калия, цой данил" },
                {"инициатор", "конушбекова калия" }
            };

        static Dictionary<string, string> закон_об_уничтожении_человечества = new Dictionary<string, string>()
            {
                { "за", ""},
                {"против", "токтосунова айназик, конушбекова калия, цой данил" },
                {"не был", "" },
                {"инициатор", "токтосунова айназик" }
            };



        public Dictionary<string, Dictionary<string, string>> Laws = new Dictionary<string, Dictionary<string, string>>()
        {
                {"закон сохранения энергии", закон_сохранения_энергии  },
                {"закон сохранения импульса", закон_сохранения_импульса  },
                {"закон Ома", закон_Ома  },
                {"закон об принятии реализма", закон_о_принятии_реализма },
                {"закон об включении мозгов", закон_о_включении_мозгов  },
                {"закон об уничтожении человечества", закон_об_уничтожении_человечества }
        };

        












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
            //string detuty = "";
            switch(update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    var text = update.Message.Text;

                    for(int j = 0; j < depities.Count; j++)
                    {
                        if (text.ToLower() == depities[j])
                        {
                            detuty = text.ToLower();
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "законопроекты с которыми взаимодействовал(-а) " + text + " или ее(его) декларация", replyMarkup: GetButtons());
                        }
                        //else
                        //  _client.SendTextMessageAsync(update.Message.Chat.Id, "Мы не знаем такого депутата пока что");
                    }
                    
                    switch(text)
                    {
                        case "/start":
                            _client.SendTextMessageAsync(update.Message.Chat.Id, @"Привет, вы можете узнать: 
    1)результаты взаимодействия депутата с законопроектами рассмотренными в ЖК
    2)декларацию депутата

Прошу если вас осенит и у вас появятся мысли на счет функционала бота или сайта дайте нам знать, ждем все глупые(и не только) идейки. Также можете, либо лично написать любому члену команды проекта panakota, либо в любую из общих YLSP групп, будем рады прочитать любую откровенность.
Ждем от вас любой совет или предложение, любую критику, каждое субъективное мнение


Вам нужно просто ввести ФИ депутата. Пока что мне известна(-ен): Токтосунова Айназик, Цой Данил, Конушбаева Калия

п.с. регистр можно не соблюдать, но последовательность - надо(ФАМИЛИЯ потом ИМЯ");
                            break;
                        case _laws:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "голосовал(-а)", replyMarkup: GetButtonsOfStatuses());
                            break;
                        case _declaration:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "за какой год?", replyMarkup: GetButtonsForDeclaration());
                            break;
                        case _oneDateOfDiclaration:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, $"{_oneDateOfDiclaration}:\n\n{ResultAboutDeclaration(detuty, _oneDateOfDiclaration)}");
                            break;
                        case _twoDateOfDiclaration:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, $"{_twoDateOfDiclaration}:\n\n{ResultAboutDeclaration(detuty, _twoDateOfDiclaration)}");
                            break;
                        case all:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, $"{statuses[0]}:{ResultOnStatus(ListLaws, Laws, detuty, statuses[0])}\n" +
                                $"{statuses[1]}:{ResultOnStatus(ListLaws, Laws, detuty, statuses[1])}\n" +
                                $"{statuses[2]}:{ResultOnStatus(ListLaws, Laws, detuty, statuses[2])}\n" +
                                $"{statuses[3]}:{ResultOnStatus(ListLaws, Laws, detuty, statuses[3])}\n");
                            break;

                    }

                    for(int j = 0; j < statuses.Length; j++)
                    {
                        if(text == statuses[j])
                        {
                            _client.SendTextMessageAsync(update.Message.Chat.Id, $"{statuses[j]}:{ResultOnStatus(ListLaws, Laws, detuty, statuses[j])}");
                        }
                        

                    }

                    //_client.SendTextMessageAsync(update.Message.Chat.Id, "Recive text : " + text, replyMarkup: GetButtons())  ;
                    break;
                default:
                    Console.WriteLine($"{update.Type} not implemented" );
                    break;
            }
        }

        private IReplyMarkup GetButtonsForDeclaration()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {

                        new List<KeyboardButton>{new KeyboardButton { Text = _oneDateOfDiclaration }},
                        new List<KeyboardButton>{new KeyboardButton { Text = _twoDateOfDiclaration}},
                        

                }
            };
        }

        private IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    
                        new List<KeyboardButton>{new KeyboardButton { Text = _laws}, new KeyboardButton { Text = _declaration} },
                        //new List<KeyboardButton>{new KeyboardButton { Text = TEXT_3}, new KeyboardButton { Text = TEXT_4} }
                    
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
                        new List<KeyboardButton>{new KeyboardButton { Text = all}}

                }
            };
        }

        public string ResultAboutDeclaration(string depity, string dataOfDeclaration)
        {
            for(int j = 0; j < forDeclaration.Count; j++)
            {
                if ($"{depity.ToLower()} {dataOfDeclaration}" == forDeclaration[j])
                {
                    return Declarations[forDeclaration[j]];
                }
                
            }
            return "";
        }

        public string ResultOnStatus(List<string> ListLaws, Dictionary<string, Dictionary<string, string>> Laws, string depity, string status)
        {

            string spisok = "";
            for (int i = 0; i < ListLaws.Count; i++)
            {
                Dictionary<string, string> TheLaw = new Dictionary<string, string>();
                TheLaw = Laws[ListLaws[i]];

                string statusOfLaw = TheLaw[status];
                string[] ListFromStatus = statusOfLaw.Split(", ");
                for (int k = 0; k < ListFromStatus.Length; k++)
                {
                    if (depity.ToLower() == ListFromStatus[k])
                    {
                        if (spisok == null)
                            spisok += $"\n{ListLaws[i]}";
                        else
                            spisok += $"\n{ListLaws[i]}";

                    }    
                }
                
            }
            return $"\n{spisok}";
        }

    }
}