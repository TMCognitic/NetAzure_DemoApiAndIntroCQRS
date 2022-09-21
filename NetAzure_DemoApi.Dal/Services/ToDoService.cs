using NetAzure_DemoApi.Dal.Entities;
using NetAzure_DemoApi.Dal.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tools.Connections.Databases;

namespace NetAzure_DemoApi.Dal.Services
{
    public class ToDoService
    {
        private readonly DbConnection _dbConnection;

        public ToDoService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<ToDo> Get()
        {
            using (_dbConnection)
            {
                return _dbConnection.ExecuteReaderImmediately("Select Id, Title, Done, Created From ToDo;", dr => dr.ToToDo());
            }
        }

        public int Insert(ToDo entity)
        {
            using (_dbConnection)
            {
                return _dbConnection.ExecuteNonQuery("Insert Into ToDo (Title) VALUES (@Title);", parameters: new { Title = entity.Title });
            }
        }
    }
}
