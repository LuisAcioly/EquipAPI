using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Models
{
    public class ValidationFieldViewModelOutput
    {
        public IEnumerable<string> Errors { get; private set; }

        public ValidationFieldViewModelOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
