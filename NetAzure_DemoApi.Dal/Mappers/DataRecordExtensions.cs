using NetAzure_DemoApi.Dal.Entities;
using System.Data;

namespace NetAzure_DemoApi.Dal.Mappers
{
    internal static class DataRecordExtensions
    {
        internal static ToDo ToToDo(this IDataRecord reader)
        {
            return new ToDo()
            {
                Id = (int)reader["Id"],
                Title = (string)reader["Title"],
                Done = (bool)reader["Done"],
                Created = (DateTime)reader["Created"]
            };
        }
    }
}
