using LeaderBoardTracker.Interfaces;
using LeaderBoardTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.MockTest
{
    public class MockPlayerRepo : IPlayerRepo
    {
        public IEnumerable<Player> GetAllPlayers()
        {
            var players = new List<Player>
            {
                new Player {Id = 1, FirstName = "John", LastName = "Smith", Email = "john.smith@abc.com"},
                new Player {Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@abc.com"},
                new Player {Id = 3, FirstName = "Poppy", LastName = "Doe", Email = "poppy.doe@xyz.com"}
            };
            return players;
        }

        public Player GetPlayerById(int Id)
        {
            return new Player { Id = 1, FirstName = "John", LastName = "Smith", Email = "john.smith@abc.com" };

        }

        public void AddPlayer(Player player)
        {
            //var Player = new Player
            //{
            //    Id = 4,
            //    FirstName = "Dylan",
            //    LastName = "Jones",
            //    Email = "dylan.jones@xyz.com"
            //};

        }

        public void UpdatePlayer(Player player)
        {

        }

        public void RemovePlayer(Player player)
        {

        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public Player GetPlayerByName(string firstName, string lastName)
        {
            return new Player { Id = 1, FirstName = "John", LastName = "Smith", Email = "john.smith@abc.com" };
        }
    }
}
