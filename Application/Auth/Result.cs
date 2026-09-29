using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        public static Result Success() => new Result { IsSuccess = true };

        public static Result Failure(IEnumerable<string> errors) =>
            new Result { IsSuccess = false, Errors = errors };

        public static Result Failure(string error) =>
            new Result { IsSuccess = false, Errors = new List<string> { error } };
    }
}
