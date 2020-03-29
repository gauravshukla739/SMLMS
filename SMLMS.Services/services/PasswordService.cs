using Microsoft.AspNetCore.Identity;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class PasswordService : IPasswordService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public PasswordService(
            UserManager<IdentityUser> userManager,IEmailService emailService)
        {        
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task<ServiceResponse> Change(ChangePasswordDto model)
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
                    var callbackUrl = $"/Password/Reset?token={token}&email={EmailId}";

                   var email = new EmailDto() {ToEmail= EmailId,Subject= "Reset password token", Body=callbackUrl };
                    var emailSend=_emailService.Send(email);
                    if (emailSend)
                    {
                        response.IsSuccess = true;
                        response.Message = "Your Reset Password Link sent to your registered EmailId.";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Issue occur in sending Reset password link.";
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

                    var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
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
    }
}
