using System;
using System.Linq;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Domain.UserManagement.ValueObjects;
using Futbal.Mng.Domain.ValueObjects;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Game_AssignNewAttendeToGame()
        {
            //Arrange
            var expectedOwner = "login";
            var expectedUser = "one_attendee";

            var owner = new User("login", "password", "email@com");
            var game = new Game("test_game", DateTime.UtcNow, owner);
            var user = new User("one_attendee", "password", "string@email.com");
            game.AddAttendee(owner);
            game.AddAttendee(user);

            Assert.AreEqual(2, game.Attendees.Count());

            Assert.IsTrue(game.Attendees.First(x => x.User.Username == "login").User.Username == expectedOwner);
            Assert.IsTrue(game.Attendees.First(x => x.User.Username == "one_attendee").User.Username == expectedUser);
        }

        [Test]
        public void Game_CreatingNewGameShouldAssignedToExistedUser()
        {
            //Arrange
            var owner = new User("login", "password", "email@com");

            //Act
            var game = new Game("test_game", DateTime.UtcNow, owner);
            
            //Assert
            Assert.AreEqual(owner.Id, game.Owner.Id);
        }

        [Test]
        public void Game_SetPlaceOfAGame()
        {
            //Arrange
            var expectedPlace = Address.Create("streetName", 1);
            var owner = new User("login", "password", "email");
            var game = new Game("game_name", new DateTime(2019, 07, 01).AddHours(19), owner);

            //Act
            game.UpdatePlace(Address.Create("streetName", 1));

            //Assert
            Assert.IsTrue(game.Place.Equals(expectedPlace));
        }

        [Test]
        public void Testing_ValueObject_primitiveobsession()
        {
            var skip = Skip.Create(1);

            bool? test = skip;
            string test2 = skip;
            int? test3 = skip;
            Skippability test4 = skip;

            Assert.AreEqual(true, test);
            Assert.AreEqual(1, test3);
            Assert.AreEqual("true", test2);
            Assert.AreEqual(Skippability.Skip, test4);
        }
    }
}