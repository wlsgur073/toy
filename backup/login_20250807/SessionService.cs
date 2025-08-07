using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhUtils.DB.Session
{
    public class SessionService
    {
        private readonly ISessionRepository _repo;
        private readonly ILogoutNotifier _notifier;

        public SessionService(ISessionRepository repo, ILogoutNotifier notifier)
        {
            this._repo = repo;
            this._notifier = notifier;
        }

        public bool HandleDuplicateLogin(
            string userId,
            string currentMachineId,
            Func<string, string, bool> confirm // (existingMachineId, userId) => YES/NO
        )
        {
            var active = _repo.GetActiveSession(userId);
            if (active == null) return true;

            var (existingToken, existingMachineId) = active.Value;
            if (string.Equals(existingMachineId, currentMachineId, StringComparison.OrdinalIgnoreCase))
                return true; // 같은 PC면 허용(재로그인 등)

            // 다른 PC → 사용자 확인
            bool yes = confirm(existingMachineId, userId);
            if (!yes) return false;

            _repo.InvalidateSession(existingToken);
            _notifier.NotifyForceLogout(existingToken);
            return true;
        }

        public void RegisterSession(string userId, string token, string ip, string machineId)
            => _repo.InsertSession(userId, token, ip, machineId);
    }
}
