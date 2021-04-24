﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using oxygen_tracker.Constants;
using oxygen_tracker.Entities;
using oxygen_tracker.Models;
using oxygen_tracker.Services;
using oxygen_tracker.Settings;
using oxygen_tracker.Settings.Models;
using oxygen_tracker.Settings.Models.Contexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace oxygen_tracker.Controllers.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IVerification _verification;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IOptions<JWT> jwt,
            ApplicationDbContext context,
            IVerification verification,
            IJwtTokenService jwtTokenService,
            IMapper mapper)
        {
            _context = context;
            _verification = verification;
            this._mapper = mapper;
            _userManager = userManager;
            _jwt = jwt.Value;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserDetail> GetUserInfoAsync(string phoneNumber)
        {
            var userDetails = _mapper.Map<UserDetail>(await _userManager.FindByNameAsync(phoneNumber));
            //userDetails ??= new UserDetail { PhoneNumber = phoneNumber };
            //var smsVerificationResult = await _verification.StartVerificationAsync(DefaultValues.IndianCode + phoneNumber);
            //if (!smsVerificationResult.IsValid) userDetails.ErrorCodes = smsVerificationResult.Errors;
            return userDetails;
        }

        public async Task<AuthenticationModel> RegisterAsync(RegisterModel model)
        {
            //var smsVerificationResult = await _verification.CheckVerificationAsync(DefaultValues.IndianCode + model.Phonenumber, model.OTP);
            var authenticationModel = new AuthenticationModel();

            //if (!smsVerificationResult.IsValid)
            //{
            //    authenticationModel.ErrorCodes = smsVerificationResult.Errors;
            //    return authenticationModel;
            //}
            var user = new ApplicationUser
            {
                UserName = model.Username,
                PhoneNumber = model.Phonenumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
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
            authenticationModel.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = await _jwtTokenService.CreateJwtToken(user);
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


        //public async Task<AuthenticationModel> RefreshTokenAsync(string jwtToken)
        //{
        //    var authenticationModel = new AuthenticationModel();
        //    var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == jwtToken));
        //    if (user == null)
        //    {
        //        authenticationModel.IsAuthenticated = false;
        //        authenticationModel.Message = $"Token did not match any users.";
        //        return authenticationModel;
        //    }

        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == jwtToken);

        //    if (!refreshToken.IsActive)
        //    {
        //        authenticationModel.IsAuthenticated = false;
        //        authenticationModel.Message = $"Token Not Active.";
        //        return authenticationModel;
        //    }

        //    //Revoke Current Refresh Token
        //    refreshToken.Revoked = DateTime.UtcNow;

        //    //Generate new Refresh Token and save to Database
        //    var newRefreshToken = _jwtTokenService.CreateRefreshToken();
        //    user.RefreshTokens.Add(newRefreshToken);
        //    _context.Update(user);
        //    _context.SaveChanges();

        //    //Generates new jwt
        //    authenticationModel.IsAuthenticated = true;
        //    JwtSecurityToken jwtSecurityToken = await _jwtTokenService.CreateJwtToken(user);
        //    authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        //    authenticationModel.Email = user.Email;
        //    authenticationModel.UserName = user.UserName;
        //    var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        //    authenticationModel.Roles = rolesList.ToList();
        //    authenticationModel.RefreshToken = newRefreshToken.Token;
        //    authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
        //    return authenticationModel;
        //}

        //public bool RevokeToken(string token)
        //{
        //    var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

        //    // return false if no user found with token
        //    if (user == null) return false;

        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        //    // return false if token is not active
        //    if (!refreshToken.IsActive) return false;

        //    // revoke token and save
        //    refreshToken.Revoked = DateTime.UtcNow;
        //    _context.Update(user);
        //    _context.SaveChanges();

        //    return true;
        //}
    }
}