using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.Models
{
    //Model class for LeaderBoard table
    public class LeaderBoard
    {
        [Key]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        [Required]
        public int GamesPlayed { get; set; }

        public int TotalScore { get; set; }
    }
}
