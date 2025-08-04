using MetaWorks.Login;
using MetaWorks.Login.Models;
using MetaWorks.Login.Services;
using MetaWorks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorks.Services
{
    public class SessionService : ISessionService
    {
        private readonly LoginSystemContext _context;
        private readonly IAuthenticationService _authService;

        public SessionService(LoginSystemContext context, IAuthenticationService authService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            try
            {
                // 1. 자격 증명 검증
                if (!_authService.ValidateCredentials(request.UserId, request.Password))
                {
                    return new LoginResult
                    {
                        ResultType = LoginResultType.InvalidCredentials,
                        Message = "아이디 또는 비밀번호가 올바르지 않습니다."
                    };
                }

                // 2.트랜잭션 시작
                using (var transaction = _context.Database.BeginTransaction())
                {
                    // 3. 기존 활성 세션 확인
                    var existingSession = await _context.UserSessions
                    .Where(s => s.UserId == request.UserId && s.IsActive)
                    .FirstOrDefaultAsync();

                    if (existingSession != null && !request.ForceLogin)
                    {
                        return new LoginResult
                        {
                            ResultType = LoginResultType.DuplicateSession,
                            RequiresConfirmation = true,
                            ExistingMachineName = existingSession.MachineId,
                            Message = $"다른 PC({existingSession.MachineId})에서 로그인 중입니다. 강제 종료하고 이 PC에서 로그인하시겠습니까?"
                        };
                    }

                    // 3. 강제 로그인 시 기존 세션 종료
                    if (existingSession != null && request.ForceLogin)
                    {
                        existingSession.IsActive = false;
                        existingSession.LastActive = DateTime.UtcNow;
                        _context.Entry(existingSession).State = EntityState.Modified;
                    }

                    // 4. 새로운 세션 생성
                    var newSession = new UserSession
                    {
                        UserId = request.UserId,
                        SessionToken = Guid.NewGuid(),
                        IsActive = true,
                        LoginTime = DateTime.UtcNow,
                        LastActive = DateTime.UtcNow,
                        IpAddress = request.IpAddress,
                        MachineId = request.MachineName
                    };

                    _context.UserSessions.Add(newSession);
                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return new LoginResult
                    {
                        ResultType = LoginResultType.Success,
                        SessionToken = newSession.SessionToken,
                        Message = "로그인에 성공했습니다."
                    };
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[LoginAsync 예외] {ex}");
                return new LoginResult
                {
                    ResultType = LoginResultType.Error,
                    Message = $"An error occurred during login: {ex.Message}"
                };
            }
        }

        public async Task<bool> ValidateSessionAsync(Guid sessionToken)
        {
            // Guid는 값 형식이므로 null이나 empty가 될 수 없지만, Guid.Empty일 수 있음
            if (sessionToken == Guid.Empty)
                return false;

            try
            {
                var session = await _context.UserSessions
                    .Where(s => s.SessionToken == sessionToken && s.IsActive)
                    .FirstOrDefaultAsync();

                if (session == null) 
                    return false;

                // 활동 갱신 시간 비교 (변경된 경우에만 save)
                var now = DateTime.UtcNow;
                if ((now - session.LastActive).TotalMinutes > 1) // 예: 1분 이상 활동이 없으면 false
                {
                    session.LastActive = now;
                    _context.Entry(session).State = EntityState.Modified;
                    var affected = await _context.SaveChangesAsync();

                    if (affected <= 0)
                    {
                        // 로그 남기기 또는 예외 고려
                        Console.Error.WriteLine($"[ValidateSessionAsync] 세션 갱신 실패: {sessionToken}");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // 예외 삼킴은 디버깅을 어렵게 만듦 (로깅 필수)
                Console.Error.WriteLine($"[세션 검증 오류] {ex.Message}");
                return false;
            }
        }

        public Task<string> GetActiveMachineNameAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserLoggedInAsync(string userId)
        {
            throw new NotImplementedException();
        }
        

        public Task LogoutAsync(Guid sessionToken)
        {
            throw new NotImplementedException();
        }

        
    }
}
