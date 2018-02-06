using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.sdk
{
    public class RealexValidationException : RealexException
    {
        private static long serialVersionUID = -5328243688578577594L;

        /**
         * List of validation messages.
         */
        public List<string> ValidationMessages
        {
            get; private set;
        }
        
        /**
         * Constructor for RealexValidationException.
         * 
         * @param message
         */
        public RealexValidationException(string message) : base(message)
        {

        }

        /**
         * Constructor for RealexValidationException.
         * 
         * @param message
         * @param validationMessages List of validation failure messages
         */
        public RealexValidationException(string message, List<string> _validationMessages) : base(message)
        {
            ValidationMessages = _validationMessages;
        }
    }
}
