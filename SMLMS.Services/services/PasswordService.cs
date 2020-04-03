using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using SMLMS.Helper;
using SMLMS.Helper.AppSetting;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class PasswordService : IPasswordService
    {
        private readonly AppConfig _appConfig;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IAuthenticationService _authenticationService;

        public PasswordService(
            UserManager<ApplicationUser> userManager,IEmailService emailService,IOptions<AppConfig> appConfig, IAuthenticationService authenticationService)
        {        
            _userManager = userManager;
            _emailService = emailService;
            _appConfig = appConfig.Value;
            _authenticationService = authenticationService;
        }
        public async Task<ServiceResponse> Change(ChangePasswordDto model, ClaimsPrincipal claims)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var emailId = claims.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(emailId);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User does not exist";
                }
                else
                {
                    var resetPassResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!resetPassResult.Succeeded)
                    {
                        response.IsSuccess = false;
                        response.Message = "Old Password is Incorrect";
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.Message = "Your Password Change Successfully!";
                    } 
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> Forgot(string EmailId)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var user = await _userManager.FindByEmailAsync(EmailId);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User does not exist";
                }
                else
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var codeEncoded = Encode(token);
                    var callbackUrl = $"{_appConfig.WebUrl}/secure/password/reset?token={codeEncoded}&email={EmailId}";
                    response.IsSuccess = true;
                    response.Data = callbackUrl;
                    response.Message = "Reset Password link send to your register Id";
                    //var email = new EmailDto() {ToEmail= EmailId,Subject= "Reset password token", Body=callbackUrl };
                    //var emailSend=_emailService.Send(email);
                    //if (emailSend)
                    //{
                    //    response.IsSuccess = true;
                    //    response.Message = "Your Reset Password Link sent to your registered EmailId.";
                    //}
                    //else
                    //{
                    //    response.IsSuccess = false;
                    //    response.Message = "Issue occur in sending Reset password link.";
                    //}
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> Reset(ResetPasswordDto model)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.EmailId);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User does not exist";
                }
                else
                {
                    var codeDecodedBytes = WebEncoders.Base64UrlDecode(model.Token);
                    var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
                    var resetPassResult = await _userManager.ResetPasswordAsync(user, codeDecoded, model.Password);
                    if (!resetPassResult.Succeeded)
                    {
                        response.IsSuccess = false;
                        response.Message = "Issue occur in Password Change";
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.Message = "Your Password Reset Successfully!";
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }
        public string Encode(string code)
        {
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
            return WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
        }
    }

   
}
