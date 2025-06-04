using PacticeService.DTOs.UserDto;
using PracticeDomain.DTOs.UserDto;
using PracticeModel.Models.AuthenticationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDomain.IRepository
{
    public interface IUserRepository
    {
        Task RegisterUser(SignupRequest request);
        Task ConfirmEmailAsync(string Userid, string code);
        Task<AuthenticationModel> Login(LoginRequest request);
    }
}
