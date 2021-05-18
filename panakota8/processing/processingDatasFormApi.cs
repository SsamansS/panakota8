using System;
using System.Collections.Generic;
using System.Text;

namespace panakota8.processing
{
    class processingDatasFormApi
    {
        ConectWithAPI re = new ConectWithAPI();
        public string TheDecision(string name, Decision decision)
        {
            string answer = "";
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

        public string CheckDeputy(string text)
        {
            ConectWithAPI depities = new ConectWithAPI();

            foreach (Deputy item in depities.getResponseOfDeputies())
            {
                if (item.Name.ToLower() == text.ToLower())
                {
                    return item.Name;
                } 
            }
            return "f";
        }

        /*public Decision CheckDecision(string status)
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
        }*/
    }
}
