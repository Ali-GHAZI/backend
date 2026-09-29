using Application.Auth;
using Application.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null ||
                !await _userManager.CheckPasswordAsync(user, password))
                throw new UnauthorizedAccessException();

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtService.Generate(user.Id, roles);

            return new AuthResult(accessToken, "REFRESH_TOKEN_PLACEHOLDER");
        }

        public async Task<Result> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result.Failure(errors);
            }

            return Result.Success();
        }

        public Task<AuthResult> RefreshAsync(string refreshToken)
            => throw new NotImplementedException();

        public Task LogoutAsync(string refreshToken)
            => Task.CompletedTask;
    }
}
