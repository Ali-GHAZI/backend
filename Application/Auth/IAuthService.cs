using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<Result> RegisterAsync(string email, string password);
        Task<AuthResult> RefreshAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
    }
}
