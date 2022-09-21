using NetAzure_DemoApi.CQRS.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAzure_DemoApi.CQRS.Mappers
{
    internal static class DataRecordExtensions
    {
        internal static User ToUser(this IDataRecord record)
        {
            return new User()
            {
                Id = (int)record["Id"],
                Email = (string)record["Email"]
            };
        }
    }
}
