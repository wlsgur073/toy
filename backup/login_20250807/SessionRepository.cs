using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhUtils.DB.Session
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DBCPManager _dbcp;
        private readonly DB_POOL_TYPE _poolType;

        public SessionRepository(DBCPManager dbcp, DB_POOL_TYPE poolType) { 
            this._dbcp = dbcp;
            this._poolType = poolType;
        }

        /// <summary>
        /// 주어진 userId에 대해 활성화된 세션이 존재하는지 조회한다.
        /// 활성 세션이 없으면 null을 반환하고,
        /// 존재하면 (session_token, machine_id) 튜플을 반환한다.
        /// 중복 로그인 판별의 1차 근거로 사용된다.
        /// </summary>
        /// <param name="userId">조회할 사용자 ID</param>
        /// <returns>(SessionToken, MachineId)?</returns>
        public (string SessionToken, string MachineId)? GetActiveSession(string userId)
        {
            DBUtilDBConnection con = null;
            DBUtilDBCommand cmd = null;
            DBUtilDataReader r = null;

            con = _dbcp.GetConnection(_poolType) 
                ?? throw new Exception("DB 연결 실패");
            try
            {
                string sql = @"SELECT session_token, machine_id
                           FROM tb_user_sessions
                           WHERE user_id = :USER_ID AND is_active = true
                           LIMIT 1";
                cmd = new DBUtilDBCommand(con, sql, "USER_ID(V)");
                cmd.SetParamValue("USER_ID", userId);

                r = new DBUtilDataReader(cmd.ExecuteReader());
                if (r.Read())
                {
                    var token = r.GetString("session_token");
                    var machine = r.GetString("machine_id");
                    r.Close(); cmd.Close();
                    return (token, machine);
                }
                r.Close(); cmd.Close();
                return null;
            }
            finally {
                try { r?.Close(); } catch { }
                try { cmd?.Close(); } catch { }
                if (con != null) DBCPManager.Instance.FreeConnection(con);
            }
        }

        public void InvalidateSession(string sessionToken)
        {
            var con = _dbcp.GetConnection(_poolType);
            try
            {
                var cmd = new DBUtilDBCommand(con,
                    "UPDATE tb_user_sessions SET is_active=false WHERE session_token=:T",
                    "T(V)");
                cmd.SetParamValue("T", sessionToken);
                cmd.ExecuteNonQuery();
                cmd.Close();
            }
            finally { DBCPManager.Instance.FreeConnection(con); }
        }

        public void InsertSession(string userId, string token, string ip, string machineId)
        {
            var con = _dbcp.GetConnection(_poolType);
            try
            {
                string sql = @"INSERT INTO tb_user_sessions
                           (session_token,user_id,is_active,login_time,last_active,ip_address,machine_id)
                           VALUES (:T,:U,true,NOW(),NOW(),:IP,:M)";
                var cmd = new DBUtilDBCommand(con, sql, "T(V),U(V),IP(V),M(V)");
                cmd.SetParamValue("T", token);
                cmd.SetParamValue("U", userId);
                cmd.SetParamValue("IP", ip);
                cmd.SetParamValue("M", machineId);
                cmd.ExecuteNonQuery();
                cmd.Close();
            }
            finally { DBCPManager.Instance.FreeConnection(con); }
        }
    }
}
