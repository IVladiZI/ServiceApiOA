using Application.DTOs.Users;
using Application.Enum;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappres;
using Domain.Settings;
using Identity.Helpers;
using Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Services
{
    /// <AccountService>
    /// In this class you will have connection with the database and the configuration and validation of the authentication with the user.
    /// </AccountService>
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jWTSettings;
        private readonly IDateTimeService _dateTimeService;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jWTSettings, IDateTimeService dateTimeService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jWTSettings = jWTSettings.Value;
            _dateTimeService = dateTimeService;
        }
        /// <summary>
        /// Method to validate and make a login 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            //search for a user by email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException($"There is no account registered with the email address {request.Email}");
            }
            //If everything is correct, start the login by sending all the necessary information.
            //lockoutOnFailure prevents lockout due to wrong password.
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ApiException($"User credentials are not valid {request.Email}");
            }

            //We generate a token for the session
            JwtSecurityToken jwtSecurityToken = await GenerateJWTToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            //refresh the JWT token based on expiration times
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, message: $"Successful login {user.UserName}");
        }
        /// <summary>
        /// Logic for registering a user in the database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string ipAddress)
        {
            var userValidate = await _userManager.FindByNameAsync(request.UserName);
            if (userValidate == null)
            {
                throw new ApiException($"The user name {request.UserName} has already been registered");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                DocumentNumber = request.DocumentNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var emaillValidate = await _userManager.FindByEmailAsync(request.Email);
            if (emaillValidate == null)
            {
                throw new ApiException($"The email {request.Email} has already been registered");
            }
            else
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    return new Response<string>(user.Id, message: $"User {request.UserName} successfully registered.");
                }
                else
                {
                    throw new ApiException($"{result.Errors}.");
                }
            }

        }
        /// <summary>
        /// Logic for generating a JWT based on user data
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<JwtSecurityToken> GenerateJWTToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id),
                new Claim("ip",ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jWTSettings.Issuer,
                audience: _jWTSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jWTSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }
        /// <summary>
        /// Logic for adding more validation time to a JWT
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };
        }
        /// <summary>
        /// Logic for generating a random JWT
        /// </summary>
        /// <returns></returns>
        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
