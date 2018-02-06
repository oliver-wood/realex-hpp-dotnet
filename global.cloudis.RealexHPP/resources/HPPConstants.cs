using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.resources
{
    public class HPPConstants
    {
        /// <summary>
        /// Flag. Could have been boolean, but used in this format in the java
        /// </summary>
        public enum Flag : int { TRUE = 1, FALSE = 0 };

        /// <summary>
        /// Fraud Filter Mode options
        /// </summary>
        public enum FraudFilterMode { OFF, ACTIVE, PASSIVE };

        /// <summary>
        /// Auto-Settlement flag and mapping to output values
        /// </summary>
        public enum AutoSettleFlag { YES, NO, MULTI };
        public static Dictionary<AutoSettleFlag, string> AutoSettleFlagValues = new Dictionary<AutoSettleFlag, string>()
        {
            { AutoSettleFlag.YES, "1" },
            { AutoSettleFlag.NO, "0" },
            { AutoSettleFlag.MULTI, "MULTI" }
        };
    }
}
