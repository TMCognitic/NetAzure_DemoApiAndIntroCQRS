using NetAzure_DemoApi.Bll.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DE = NetAzure_DemoApi.Dal.Entities;

namespace NetAzure_DemoApi.Bll.Mappers
{
    internal static class Mappers
    {
        #region ToDo
        internal static DE.ToDo ToDal(this ToDo entity)
        {
            return new DE.ToDo() { Id = entity.Id, Title = entity.Title, Done = entity.Done };
        }

        internal static ToDo ToBll(this DE.ToDo entity)
        {
            return new ToDo(entity.Id, entity.Title, entity.Done);
        }
        #endregion
    }
}
