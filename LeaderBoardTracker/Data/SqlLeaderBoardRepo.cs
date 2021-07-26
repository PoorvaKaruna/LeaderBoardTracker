using System;
using System.Collections.Generic;
using System.Linq;
using LeaderBoardTracker.Interfaces;
using LeaderBoardTracker.Messages;
using LeaderBoardTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaderBoardTracker.Data
{
    public class SqlLeaderBoardRepo : ILeaderBoardRepo
    {
        private readonly LeaderBoardTrackerContext _context;

        public SqlLeaderBoardRepo(LeaderBoardTrackerContext context)
        {
            _context = context;
        }
        public void AddLBEntries(LeaderBoard lbEntry)
        {
            if (lbEntry == null)
            {
                throw new ArgumentNullException(nameof(lbEntry));
            }

            _context.LeaderBoard.Add(lbEntry);
        }

        public void RemoveLBEntries(LeaderBoard lbEntry)
        {
            if (lbEntry == null)
            {
                throw new ArgumentNullException(nameof(lbEntry));
            }
            _context.LeaderBoard.Remove(lbEntry);
        }

        public Player GetPlayerInfo(int Playerid)
        {

           return _context.Player.Find(Playerid);
        }

        public IEnumerable<LeaderBoard> GetAllLBEntries()
        {
            return _context.LeaderBoard.ToList();
        }

        public IEnumerable<LeaderBoard> GetAllLBEntriesByOrder(string orderBy, string orderByKey)
        {
            List<LeaderBoard> lstDBLBEntries = new List<LeaderBoard>();
            List<LeaderBoard> lstOrderedLMEntries = new List<LeaderBoard>();
            lstDBLBEntries = _context.LeaderBoard.ToList();
            if (String.Equals(orderBy.ToLower(), Constants.GamesPlayedForComparison.ToLower()))
            {
                if (orderByKey.ToLower() == Constants.Asc.ToLower())
                {
                    lstOrderedLMEntries = lstDBLBEntries.OrderBy(o => o.GamesPlayed).ToList();
                }
                else
                {
                    lstOrderedLMEntries = lstDBLBEntries.OrderByDescending(o => o.GamesPlayed).ToList();
                }
            }
            else
            {
                if (String.Equals(orderByKey.ToLower(),Constants.Asc.ToLower()))
                {
                    lstOrderedLMEntries = lstDBLBEntries.OrderBy(o => o.TotalScore).ToList();
                }
                else
                {
                    lstOrderedLMEntries = lstDBLBEntries.OrderByDescending(o => o.TotalScore).ToList();
                }
            }
            return lstOrderedLMEntries;
        }

        public LeaderBoard GetLBEntryById(int Id)
        {
            return _context.LeaderBoard.FirstOrDefault(p => p.Id == Id);
        }

        public LeaderBoard GetLBEntryByPlayerId(int PlayerId)
        {
            return _context.LeaderBoard.FirstOrDefault(p => p.PlayerId == PlayerId);
        }

        public bool SaveChanges()
        {
        
            return (_context.SaveChanges() >= 0);
        }
         
        public void UpdateLBEntries(LeaderBoard lbEntry)
        {
            _context.Entry(lbEntry).Property(u => u.PlayerId).IsModified = false;
            _context.LeaderBoard.Update(lbEntry);
        }
    }
}