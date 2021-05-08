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

            /*foreach (Deputy item in re.getResponseOfDeputies())
            {
                if (item.Name.ToLower() == name.ToLower())
                {
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
            return answer;*/

            DeputyActivity deputyActivity = re.getResponseOfDeputy(name);

            if (deputyActivity.Name.ToLower() == name.ToLower())
            {
                foreach (Vote item in deputyActivity.Votes)
                {
                    if(item.Decision == decision)
                        answer += item.Law.LawName + "\n";
                }
            }
            Console.WriteLine(answer);
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

            Decision[] decisions = { Decision.Absent, Decision.Agreed, Decision.Rejected, Decision.Initiator };

            return processing.TheDecision(name, CheckDecision(status));
        }
    }
}
