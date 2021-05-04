using System;
using System.Collections.Generic;
using System.Text;

namespace panakota8.processing
{
    class processingDatasFormApi
    {
        public string TheDecision(string name, Decision decision)
        {
            ConectWithAPI re = new ConectWithAPI();
            string answer = "";

            foreach (Deputy item in re.getResponseOfDeputies())
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    /*if (decision == Decision.Absent)
                        Console.WriteLine("Отсутствовал:");
                    else if (decision == Decision.Agreed)
                        Console.WriteLine("За:");
                    else if (decision == Decision.Initiator)
                        Console.WriteLine("Инициировал:");
                    else if (decision == Decision.Rejected)
                        Console.WriteLine("Против:");*/

                    foreach (Vote itemm in re.getResponseOfDeputy(item.Name))
                    {
                        if (itemm.Decision == decision && itemm.DeputyId == item.Id)
                        {
                            foreach (Law itemOfLaw in re.getResponseOfLaws())
                            {
                                if (itemm.LawId == itemOfLaw.Id)
                                {
                                    answer += $"{itemOfLaw.LawName}\n";
                                }
                            }
                        }
                    }
                }
            }
            return answer;
        }

        public Decision CheckDecision(string status)
        {
            if (status.ToLower() == "отсутствовал")
                return Decision.Absent;
            else if (status.ToLower() == "за")
                return Decision.Agreed;
            else if (status.ToLower() == "инициатор")
                return Decision.Initiator;
            else if (status.ToLower() == "против")
                return Decision.Rejected;
            else
                return Decision.Nothing;
        }
        public string Process(string name, string status)
        {
            processingDatasFormApi processing = new processingDatasFormApi();

            //Console.WriteLine(processing.TheDecision(name, CheckDecision(status)));

            Decision[] decisions = { Decision.Absent, Decision.Agreed, Decision.Rejected, Decision.Initiator };

            /*if (status == "все")
            {
                foreach (Decision item in decisions)
                {

                    Console.WriteLine(processing.TheDecision(name, item));
                }
            }*/
            return processing.TheDecision(name, CheckDecision(status));
        }
    }
}
