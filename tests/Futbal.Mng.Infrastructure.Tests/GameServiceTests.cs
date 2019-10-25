using NUnit.Framework;
using Moq;
using AutoMapper;
using Futbal.Mng.Infrastructure.GameManagement;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using System;
using System.Threading.Tasks;
using System.Linq;
using Futbal.Mng.Infrastructure.DTO;
using System.Collections.Generic;

namespace Tests
{
    public class GameServiceTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IGameRepository> _gameRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        
        public GameServiceTests()
        {
            _gameRepoMock = new Mock<IGameRepository>();
            _mapper = new Mock<IMapper>();
            _userRepoMock = new Mock<IUserRepository>();
            
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetUserGames_ShouldReturn_AllGamesThatUserBePartOf()
        {
            //Arrange
            var sut = new GameService(_gameRepoMock.Object, _userRepoMock.Object, _mapper.Object);
            _mapper.Setup(m => m.Map<IList<GameDetailsGridDto>>(It.IsAny<object>()))
                .Returns(new List<GameDetailsGridDto>{
                    new GameDetailsGridDto()
                });
                var ownerUser = new User("userName", "password", "email");
            _gameRepoMock.Setup(m => m.GetUserGames(It.IsAny<Guid>()))
                .ReturnsAsync(new List<Game>{
                    new Game("gamename", DateTime.Now, ownerUser)
                });

            //Act
            var result = await sut.GetUserGames(It.IsAny<Guid>());
            
            //Assert
            Assert.AreEqual(1, result.Count());
        }
    }
}