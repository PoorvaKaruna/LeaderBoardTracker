using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.DTOs
{
    public class AddOrUpdateLeaderBoardDTO
    {
        public List<LeaderBoardDTO> LstLeaderBoardDTO { get; set; }

    }

    public class LeaderBoardDTO
    {
        public int PlayerId { get; set; }       //Assuming only one entry for a player in the leaderboard.

        [Required(ErrorMessage = "GamesPlayed is a required field.")]
        public int GamesPlayed { get; set; }

        public int TotalScore { get; set; }
    }
}
