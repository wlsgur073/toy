using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhUtils.DB.Session
{
    public interface ISessionRepository
    {
        (string SessionToken, string MachineId)? GetActiveSession(string userId);
        void InvalidateSession(string sessionToken);
        void InsertSession(string userId, string sessionToken, string ip, string machineId);
    }

    public interface ILogoutNotifier
    {
        void NotifyForceLogout(string sessionToken);
    }

    public interface IForceLogoutListener : IDisposable
    {
        void Start(string connString, string currentSessionToken, Action onForceLogout);
        void Stop();
    }
}
