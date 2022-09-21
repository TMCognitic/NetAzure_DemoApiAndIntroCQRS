using NetAzure_DemoApi.CQRS.Entities;
using NetAzure_DemoApi.CQRS.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Connections.Databases;
using Tools.CQRS.Queries;

namespace NetAzure_DemoApi.CQRS.Queries
{
    public class LoginQuery : IQuery<User>
    {
        public string Email { get; init; }
        public string Passwd { get; init; }
        public LoginQuery(string email, string passwd)
        {
            Email = email;
            Passwd = passwd;
        }
    }

    public class LoginQueryHandler : IQueryHandler<LoginQuery, User>
    {
        private readonly DbConnection _dbConnection;

        public LoginQueryHandler(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public User? Execute(LoginQuery query)
        {
            using (_dbConnection)
            {
                return _dbConnection.ExecuteReader("CSP_Login", dr => dr.ToUser(), true, query).SingleOrDefault();
            }
        }
    }
}
