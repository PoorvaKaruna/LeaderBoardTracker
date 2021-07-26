using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.DTOs
{
    public class ResponseLeaderBoardDTO
    {
        public int PlayerId { get; set; }       //Assuming only one entry for a player in the leaderboard.
        public int GamesPlayed { get; set; }
        public int TotalScore { get; set; }
        public string Operation { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
