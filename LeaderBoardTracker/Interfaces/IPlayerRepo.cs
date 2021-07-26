using LeaderBoardTracker.DTOs;
using LeaderBoardTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.Interfaces
{
    public interface IPlayerRepo
    {
        IEnumerable<Player> GetAllPlayers();
        Player GetPlayerById(int Id);

        Player GetPlayerByName(string firstName, string lastName);

        void AddPlayer(Player player);

        void UpdatePlayer(Player player);

        void RemovePlayer(Player player);

        bool SaveChanges();
    }
}
