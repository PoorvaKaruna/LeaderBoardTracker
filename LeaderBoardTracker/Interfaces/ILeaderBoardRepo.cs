using LeaderBoardTracker.DTOs;
using LeaderBoardTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.Interfaces
{
    public interface ILeaderBoardRepo
    {
        IEnumerable<LeaderBoard> GetAllLBEntries();

        LeaderBoard GetLBEntryById(int Id);

        LeaderBoard GetLBEntryByPlayerId(int PlayerId);

        IEnumerable<LeaderBoard> GetAllLBEntriesByOrder(string orderBy, string orderByKey);

        Player GetPlayerInfo(int PlayerId);
        void AddLBEntries(LeaderBoard lbEntry);

        void UpdateLBEntries(LeaderBoard lbEntry);

        void RemoveLBEntries(LeaderBoard lbEntry);

        bool SaveChanges();
    }
}
