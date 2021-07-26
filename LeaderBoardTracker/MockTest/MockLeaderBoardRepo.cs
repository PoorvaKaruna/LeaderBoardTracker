using LeaderBoardTracker.Interfaces;
using LeaderBoardTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.MockTest
{
    public class MockLeaderBoardRepo : ILeaderBoardRepo
    {
        public void AddLBEntries(LeaderBoard lbEntry)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveLBEntries(LeaderBoard lbEntry)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<LeaderBoard> GetAllLBEntries()
        {
            var lbEntries = new List<LeaderBoard>
            {
                new LeaderBoard {Id = 1, PlayerId = 1, GamesPlayed = 10, TotalScore = 10},
                new LeaderBoard {Id = 2, PlayerId = 2, GamesPlayed = 8, TotalScore = 8}
            };
            return lbEntries;
        }

        public LeaderBoard GetLBEntryById(int Id)
        {
            return new LeaderBoard { Id = 1, PlayerId = 1, GamesPlayed = 10, TotalScore = 10 };

        }

        public LeaderBoard GetLBEntryByPlayerId(int PlayerId)
        {
            return new LeaderBoard { Id = 1, PlayerId = 1, GamesPlayed = 10, TotalScore = 10 };

        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateLBEntries(LeaderBoard lbEntry)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<LeaderBoard> GetAllLBEntriesByOrder(string orderBy, string orderByKey)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayerInfo(int PlayerId)
        {
            throw new NotImplementedException();
        }
    }
}
