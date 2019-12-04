using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validation
{
    public class ValidationEmailDomainAttribute:ValidationAttribute
    {
        private readonly string allowDomain;

        public ValidationEmailDomainAttribute(string allowDomain)
        {
            this.allowDomain = allowDomain;
        }
        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split('@');
            
            return strings[1].ToUpper() == allowDomain.ToUpper();
        }
    }
}
