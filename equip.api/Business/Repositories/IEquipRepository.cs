using equip.api.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Business.Repositories
{
    public interface IEquipRepository
    {
        void Add(Equip equip);

        void Commit();

        IList<Equip> GetByUser(int userCode);
    }
}
