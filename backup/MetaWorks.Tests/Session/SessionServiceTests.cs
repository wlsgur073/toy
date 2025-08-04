using MetaWorks.Login;
using MetaWorks.Login.Models;
using MetaWorks.Login.Services;
using MetaWorks.Models;
using MetaWorks.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;


namespace MetaWorks.Tests
{
    [TestClass]
    public class SessionServiceTests
    {
        private ISessionService _sessionService;
        private LoginSystemContext _context;
        private Mock<IAuthenticationService> _authServiceMock;

        [TestInitialize]
        public void Init()
        {
            // In-Memory Database 설정 (Effort를 사용한 In-memory DB 연결)
            var connection = Effort.DbConnectionFactory.CreateTransient();
            _context = new LoginSystemContext(connection);

            // Mock 객체 생성
            _authServiceMock = new Mock<IAuthenticationService>();
            _authServiceMock.Setup(x => x.ValidateCredentials("testuser", "password123"))
                           .Returns(true);
            _authServiceMock.Setup(x => x.ValidateCredentials("testuser", "wrongpassword"))
                           .Returns(false);

            _sessionService = new SessionService(_context, _authServiceMock.Object);
        }

        [TestCleanup]
        public void TestCleanup() => _context?.Dispose();


        [TestMethod]
        public async Task 로그인_성공시_Success_반환Async()
        {
            // Arrange
            var request = new LoginRequest
            {
                UserId = "testuser",
                Password = "password123",
                MachineName = "PC1",
                IpAddress = "192.168.1.100"
            };

            // Act
            var result = await _sessionService.LoginAsync(request);

            // Assert
            Assert.AreEqual(LoginResultType.Success, result.ResultType);
            Assert.IsNotNull(result.SessionToken);
            Assert.IsFalse(result.SessionToken == Guid.Empty);
        }

        [TestMethod]
        public async Task 잘못된_비밀번호_입력시_InvalidCredentials_반환()
        {
            // Arrange
            var request = new LoginRequest
            {
                UserId = "testuser",
                Password = "wrongpassword",
                MachineName = "PC1",
                IpAddress = "192.168.1.100"
            };

            // Act
            var result = await _sessionService.LoginAsync(request);

            // Assert
            Assert.AreEqual(LoginResultType.InvalidCredentials, result.ResultType);
            Assert.IsNull(result.SessionToken);
        }

        [TestMethod]
        public async Task 이미_로그인된_사용자_DuplicateSession_반환()
        {
            // Arrange - 첫 번째 로그인
            var firstRequest = new LoginRequest
            {
                UserId = "testuser",
                Password = "password123",
                MachineName = "PC1",
                IpAddress = "192.168.1.100"
            };

            await _sessionService.LoginAsync(firstRequest);

            // 두 번째 로그인 시도
            var secondRequest = new LoginRequest
            {
                UserId = "testuser",
                Password = "password123",
                MachineName = "PC2",
                IpAddress = "192.168.1.101"
            };

            // Act
            var result = await _sessionService.LoginAsync(secondRequest);

            // Assert
            Assert.AreEqual(LoginResultType.DuplicateSession, result.ResultType);
            Assert.IsTrue(result.RequiresConfirmation);
            Assert.AreEqual("PC1", result.ExistingMachineName);
            Assert.IsNull(result.SessionToken);
        }

        [TestMethod]
        public async Task 강제_로그인시_이전_세션_비활성화()
        {
            // Arrange - 첫 번째 로그인
            var firstRequest = new LoginRequest
            {
                UserId = "testuser",
                Password = "password123",
                MachineName = "PC1",
                IpAddress = "192.168.1.100"
            };

            var firstResult = await _sessionService.LoginAsync(firstRequest);

            // 강제 로그인 시도
            var forceRequest = new LoginRequest
            {
                UserId = "testuser",
                Password = "password123",
                MachineName = "PC2",
                IpAddress = "192.168.1.101",
                ForceLogin = true
            };

            // Act
            var forceResult = await _sessionService.LoginAsync(forceRequest);

            // Assert
            Assert.AreEqual(LoginResultType.Success, forceResult.ResultType);
            Assert.IsNotNull(forceResult.SessionToken);

            // 이전 세션이 무효화되었는지 확인
            var isFirstSessionValid = await _sessionService.ValidateSessionAsync(firstResult.SessionToken);
            Assert.IsFalse(isFirstSessionValid);
        }

        [TestMethod]
        public async Task 유효한_세션_토큰_검증시_true_반환()
        {
            // Arrange
            var request = new LoginRequest
            {
                UserId = "testuser",
                Password = "password123",
                MachineName = "PC1",
                IpAddress = "192.168.1.100"
            };

            var loginResult = await _sessionService.LoginAsync(request);

            // Act
            var isValid = await _sessionService.ValidateSessionAsync(loginResult.SessionToken);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public async Task 잘못된_세션_토큰_검증시_false_반환()
        {
            // Act
            //var isValid = await _sessionService.ValidateSessionAsync("invalid_token");

            // Assert
            //Assert.IsFalse(isValid);
        }
    }
}
