﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.DTOs
{
    public class ResponsePlayerDTOToRead
    {   
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Operation { get; set; }
    }
}
