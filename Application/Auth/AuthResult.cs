using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth
{
    public sealed record AuthResult(
        string AccessToken,
        string RefreshToken
    );
}
