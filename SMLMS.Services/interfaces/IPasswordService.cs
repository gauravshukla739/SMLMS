using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.DTO;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface IPasswordService
    {
        Task<ServiceResponse> Forgot(string EmailId);

        Task<ServiceResponse> Reset(ResetPasswordDto model);

        Task<ServiceResponse> Change(ChangePasswordDto model);
    }
}
