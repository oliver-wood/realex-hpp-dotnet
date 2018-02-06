using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.resources
{
    public class HPPMessages
    {
        public static Dictionary<string, string> Messages;

        public const string Error1 = "";

        static HPPMessages()
        {
            Messages = new Dictionary<string, string>();
            Messages = (Dictionary<string, string>)ConfigurationManager.GetSection("hpp-configuration");
        }
    }
}
