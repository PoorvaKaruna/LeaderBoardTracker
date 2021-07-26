using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.DTOs
{
    public class ResponseDeletePlayerDTO
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public int StatusCode { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}
