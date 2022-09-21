using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Connections.Databases;
using Tools.CQRS.Commands;

namespace NetAzure_DemoApi.CQRS.Commands
{
    public class RegisterCommand : ICommand
    {
        public string Email { get; init; }
        public string Passwd { get; init; }
        public RegisterCommand(string email, string passwd)
        {
            Email = email;
            Passwd = passwd;
        }
    }

    public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly DbConnection _dbConnection;

        public RegisterCommandHandler(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Result Execute(RegisterCommand command)
        {
            try
            {
                using (_dbConnection)
                {
                    int rows = _dbConnection.ExecuteNonQuery("CSP_Register", true, command);

                    if (rows != 1)
                        return Result.Failure("Nombre de record incorrecte");

                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex);
            }
            
        }
    }

}
