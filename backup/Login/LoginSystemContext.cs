using MetaWorks.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorks.Login
{
    public class LoginSystemContext : DbContext
    {
        public LoginSystemContext() : base("name=DefaultConnection")
        {}

        public LoginSystemContext(string connectionString) : base(connectionString)
        {}
        public LoginSystemContext(DbConnection connection) : base(connection, true) { } // ← 테스트용 생성자

        public virtual DbSet<UserSession> UserSessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSession>()
                .HasIndex(u => u.UserId)
                .HasName("IX_TB_USER_SESSIONS_USER_ID");

            modelBuilder.Entity<UserSession>()
                .HasIndex(u => u.SessionToken)
                .HasName("IX_TB_USER_SESSIONS_SESSION_TOKEN");

            base.OnModelCreating(modelBuilder);
        }
    }
}
