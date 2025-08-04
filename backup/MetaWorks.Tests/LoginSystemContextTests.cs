using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MetaWorks.Login;
using MetaWorks.Models;
using System.Data;

namespace MetaWorks.Tests
{
    [TestClass]
    public class LoginSystemContextTests
    {
        public TestContext TestContext { get; set; } // 테스트 출력용
        private LoginSystemContext _context;

        [TestInitialize]
        public void Init()
        {
            // Effort를 사용한 In-memory DB 연결
            var connection = Effort.DbConnectionFactory.CreateTransient();
            _context = new LoginSystemContext(connection);
        }

        [TestMethod]
        public void 동일한_UserId_로그인시_이전_세션_비활성화되고_새로운_세션_생성됨()
        {
            // Arrange
            var userId = "dup-user";

            var oldSession = new UserSession
            {
                UserId = userId,
                SessionToken = Guid.NewGuid(),
                IsActive = true,
                LoginTime = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                IpAddress = "127.0.0.1",
                MachineId = "PC1"
            };

            _context.UserSessions.Add(oldSession);
            _context.SaveChanges();

            // 기존 세션 비활성화
            var existingSessions = _context.UserSessions.Where(u => u.UserId == userId && u.IsActive);
            foreach (var s in existingSessions)
                s.IsActive = false;

            var newSession = new UserSession
            {
                UserId = userId,
                SessionToken = Guid.NewGuid(),
                IsActive = true,
                LoginTime = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                IpAddress = "127.0.0.2",
                MachineId = "PC2"
            };

            _context.UserSessions.Add(newSession);
            _context.SaveChanges();

            // Assert
            var sessions = _context.UserSessions.Where(s => s.UserId == userId).ToList();
            Assert.AreEqual(2, sessions.Count);
            Assert.AreEqual(1, sessions.Count(s => s.IsActive));
            Assert.AreEqual("PC2", sessions.First(s => s.IsActive).MachineId);
        }

        [TestMethod]
        public void 활성_세션만_조회_가능해야_함()
        {
            // Arrange
            _context.UserSessions.Add(new UserSession
            {
                UserId = "u1",
                SessionToken = Guid.NewGuid(),
                IsActive = true,
                LoginTime = DateTime.Now,
                LastActive = DateTime.Now,
                IpAddress = "1.1.1.1",
                MachineId = "PC1"
            });

            _context.UserSessions.Add(new UserSession
            {
                UserId = "u2",
                SessionToken = Guid.NewGuid(),
                IsActive = false,
                LoginTime = DateTime.Now,
                LastActive = DateTime.Now,
                IpAddress = "1.1.1.2",
                MachineId = "PC2"
            });

            _context.SaveChanges();

            // Act
            var activeSessions = _context.UserSessions.Where(s => s.IsActive).ToList();

            // Assert
            Assert.AreEqual(1, activeSessions.Count);
            Assert.AreEqual("u1", activeSessions[0].UserId);
        }

        [TestMethod]
        public void UserSession_Insert_And_Retrieve()
        {
            // Arrange
            _context.UserSessions.Add(new UserSession
            {
                UserId = "test1",
                SessionToken = Guid.NewGuid(),
                IsActive = true,
                LoginTime = DateTime.Now,
                LastActive = DateTime.Now,
                IpAddress = "127.0.0.1",
                MachineId = "PC1"
            });

            _context.SaveChanges();

            // Act
            var session = _context.UserSessions.FirstOrDefault(u => u.UserId == "test1");

            // Assert
            Assert.IsNotNull(session);
            Assert.IsTrue(session.IsActive);
        }

        [TestMethod]
        public void 세션_LastActive_갱신_정상동작()
        {
            // Arrange
            var session = new UserSession
            {
                UserId = "test-last",
                SessionToken = Guid.NewGuid(),
                IsActive = true,
                LoginTime = DateTime.Now,
                LastActive = DateTime.Now.AddMinutes(-10),
                IpAddress = "1.1.1.1",
                MachineId = "PC-X"
            };

            _context.UserSessions.Add(session);
            _context.SaveChanges();

            // Act
            var saved = _context.UserSessions.First(u => u.UserId == "test-last");
            saved.LastActive = DateTime.Now;
            TestContext.WriteLine($"saved: {saved.LastActive}");
            _context.SaveChanges();

            // Assert
            var updated = _context.UserSessions.First(u => u.UserId == "test-last");
            TestContext.WriteLine($"LastActive: {updated.LastActive}");
            Assert.IsTrue((DateTime.Now - updated.LastActive).TotalSeconds < 1);
        }

        [TestMethod]
        public void 비활성_세션_삭제_정상동작()
        {
            // Arrange
            var inactive = new UserSession
            {
                UserId = "u-delete",
                SessionToken = Guid.NewGuid(),
                IsActive = false,
                LoginTime = DateTime.Now,
                LastActive = DateTime.Now,
                IpAddress = "192.168.1.10",
                MachineId = "DELETE-PC"
            };
            _context.UserSessions.Add(inactive);
            _context.SaveChanges();

            // Act
            var target = _context.UserSessions.Single(u => u.UserId == "u-delete");
            _context.UserSessions.Remove(target);
            _context.SaveChanges();

            // Assert
            Assert.IsFalse(_context.UserSessions.Any(u => u.UserId == "u-delete"), "비활성 세션이 삭제되지 않았습니다.");
        }

        [TestMethod]
        public void 로그인시간_내림차순_정렬_정상작동()
        {
            // Arrange
            _context.UserSessions.Add(new UserSession
            {
                UserId = "u1",
                SessionToken = Guid.NewGuid(),
                IsActive = true,
                LoginTime = DateTime.Now.AddMinutes(-10),
                LastActive = DateTime.Now,
                IpAddress = "1.1.1.1",
                MachineId = "PC1"
            });

            _context.UserSessions.Add(new UserSession
            {
                UserId = "u2",
                SessionToken = Guid.NewGuid(),
                IsActive = true,
                LoginTime = DateTime.Now,
                LastActive = DateTime.Now,
                IpAddress = "1.1.1.2",
                MachineId = "PC2"
            });

            _context.SaveChanges();

            // Act
            var ordered = _context.UserSessions
                .OrderByDescending(u => u.LoginTime)
                .Select(u => u.UserId)
                .ToList();

            // Assert
            Assert.AreEqual("u2", ordered.First());
        }

        [TestMethod]
        public void 존재하지_않는_사용자_조회시_null_반환()
        {
            // Act
            var result = _context.UserSessions.FirstOrDefault(u => u.UserId == "non-existent");

            // Assert
            Assert.IsNull(result);
        }

    }

}
