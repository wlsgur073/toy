using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorks.Login.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// 사용자 자격 검증 (예: DB/하드코딩 등)
        /// </summary>
        bool ValidateCredentials(string userId, string password);
    }
}
