using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using oxygen_tracker.Constants;
using oxygen_tracker.Models;
using oxygen_tracker.Services.Interface;
using oxygen_tracker.Settings;
using oxygen_tracker.Settings.Models;
using oxygen_tracker.Settings.Models.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace oxygen_tracker.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IVerification _verification;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IVerification verification,
            IJwtTokenService jwtTokenService,
            IMapper mapper)
        {
            _context = context;
            _verification = verification;
            _mapper = mapper;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserDetail> GetUserInfoAsync(string phoneNumber)
        {
            var userDetails = _mapper.Map<UserDetail>(await _userManager.FindByNameAsync(phoneNumber));
            userDetails ??= new UserDetail { PhoneNumber = phoneNumber, ErrorCodes = DefaultValues.ErrorCodes.UserNotFound };
            var smsVerificationResult = await _verification.StartVerificationAsync(DefaultValues.IndianCode + phoneNumber);
            if (!smsVerificationResult.IsValid) userDetails.ErrorCodes = smsVerificationResult.Errors;
            return userDetails;
        }

        public async Task<AuthenticationModel> RegisterAsync(RegisterModel model)
        {
            var smsVerificationResult = await _verification.CheckVerificationAsync(DefaultValues.IndianCode + model.Phonenumber, model.OTP);
            var authenticationModel = new AuthenticationModel();

            if (!smsVerificationResult.IsValid)
            {
                authenticationModel.ErrorCodes = smsVerificationResult.Errors;
                return authenticationModel;
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                PhoneNumber = model.Phonenumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            };
            var userDetail = await _userManager.FindByEmailAsync(model.Email);
            if (userDetail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
                }
                else
                {
                    authenticationModel.ErrorCodes = DefaultValues.ErrorCodes.SomethingWentWrong;
                    return authenticationModel;
                }
            }
            user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.ErrorCodes = DefaultValues.ErrorCodes.UserNotFound;
                return authenticationModel;
            }

            authenticationModel.IsAuthenticated = true;
            authenticationModel.UserId = user.Id;
            JwtSecurityToken jwtSecurityToken = await _jwtTokenService.CreateJwtTokenAsync(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.UserName = user.UserName;
            authenticationModel.Email = user.Email;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();

            if (user.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(a => a.IsActive);
                authenticationModel.RefreshToken = activeRefreshToken.Token;
                authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = _jwtTokenService.CreateRefreshToken();
                authenticationModel.RefreshToken = refreshToken.Token;
                authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                user.RefreshTokens.Add(refreshToken);
                _context.Update(user);
                _context.SaveChanges();
            }

            return authenticationModel;
        }
    }
}