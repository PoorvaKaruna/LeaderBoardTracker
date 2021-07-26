using LeaderBoardTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.Data
{
    //DbContext class used to establish connection to the database
    public class LeaderBoardTrackerContext : DbContext
    {
        public LeaderBoardTrackerContext(DbContextOptions<LeaderBoardTrackerContext> opt) : base(opt)
        {

        }

        public DbSet<Player> Player { get; set; }
        public DbSet<LeaderBoard> LeaderBoard { get; set; }
    }
}
