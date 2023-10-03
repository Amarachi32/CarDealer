using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.DTO.ResponseDto
{
    public class UserReturnDto
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
    }

    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

    }

    public class ProfileResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class AuthResponse
    {

        public string Message { get; set; }
        public string Token { get; set; }
        public LoginStatus LoginStatus { get; set; }
    }

    public enum LoginStatus
    {
        LoginFailed,
        LoginSuccessful
    }
}
