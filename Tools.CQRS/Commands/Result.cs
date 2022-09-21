using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tools.CQRS.Commands
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public string? Message { get; set; }

        private Result(bool isSuccess, string? message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Failure(string message)
        {
            return new Result(false, message);
        }

        public static Result Failure(Exception ex)
        {
            return new Result(false, JsonSerializer.Serialize(ex));
        }
    }
}
