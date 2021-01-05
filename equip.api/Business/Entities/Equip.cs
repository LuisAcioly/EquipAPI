using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Business.Entities
{
    public class Equip
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }

        public int UserCode { get; set; }

        public virtual User User { get; set; }
    }
}
