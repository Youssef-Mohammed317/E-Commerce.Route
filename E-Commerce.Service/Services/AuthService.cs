using E_Commerce.Domian.Entites.IdentityModule;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return Result<UserDTO>.Fail(Error.InvalidCrendentials("Invaild caredentials"));
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isPasswordValid)
            {
                return Result<UserDTO>.Fail(Error.InvalidCrendentials("Invaild caredentials"));
            }
            var userDTO = new UserDTO
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await GenterateTokenAsync(user)
            };

            return Result<UserDTO>.Ok(userDTO);
        }


        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                var userDTO = new UserDTO
                {
                    Email = user.Email!,
                    DisplayName = user.DisplayName,
                    Token = await GenterateTokenAsync(user)
                };
                return Result<UserDTO>.Ok(userDTO);
            }
            var errors = result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();
            return Result<UserDTO>.Fail(errors);

        }
        private async Task<string> GenterateTokenAsync(ApplicationUser user)
        {
            return "Token"; // Implement token generation logic here
        }
    }
}
