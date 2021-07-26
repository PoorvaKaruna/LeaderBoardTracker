using System;
using System.Collections.Generic;
using System.Linq;
using LeaderBoardTracker.Interfaces;
using LeaderBoardTracker.Models;

namespace LeaderBoardTracker.Data
{
    public class SqlPlayerRepo : IPlayerRepo
    {
        private readonly LeaderBoardTrackerContext _context;

        public SqlPlayerRepo(LeaderBoardTrackerContext context)
        {
            _context = context;
        }
        public void AddPlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            _context.Player.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            _context.Player.Remove(player);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _context.Player.ToList();
        }

        public Player GetPlayerById(int Id)
        {
            return _context.Player.FirstOrDefault(p => p.Id == Id);
        }

        public Player GetPlayerByName(string firstName, string lastName)
        {
            return _context.Player.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdatePlayer(Player player)
        {
            _context.Player.Update(player);
        }
    }
}