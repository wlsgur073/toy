using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorks.Login.Models
{
    public enum LoginResultType
    {
        Success,
        InvalidCredentials,
        DuplicateSession,
        Error
    }

    public class LoginResult
    {
        public LoginResultType ResultType { get; set; }
        public Guid SessionToken { get; set; }
        public string Message { get; set; }
        public bool RequiresConfirmation { get; set; }
        public string ExistingMachineName { get; set; }
    }

    public class LoginRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string MachineName { get; set; }
        public string IpAddress { get; set; }
        public bool ForceLogin { get; set; } = false;
    }
}
