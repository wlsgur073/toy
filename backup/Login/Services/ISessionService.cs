using MetaWorks.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorks.Login.Services
{
    public interface ISessionService
    {
        /// <summary>
        /// 로그인 요청 및 중복 세션 판단 등 처리
        /// </summary>
        Task<LoginResult> LoginAsync(LoginRequest request);

        /// <summary>
        /// 세션 유효성 검사
        /// </summary>
        Task<bool> ValidateSessionAsync(Guid sessionToken);

        /// <summary>
        /// 세션 종료
        /// </summary>
        Task LogoutAsync(Guid sessionToken);

        /// <summary>
        /// 사용자 로그인 여부 확인
        /// </summary>
        Task<bool> IsUserLoggedInAsync(string userId);

        /// <summary>
        /// 기존 로그인된 머신 이름 조회
        /// </summary>
        Task<string> GetActiveMachineNameAsync(string userId);
    }
}
