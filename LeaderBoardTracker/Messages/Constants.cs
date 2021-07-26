using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoardTracker.Messages
{
    public static class Constants
    {
        public const string MandatoryField = "attribute is mandatory.";
        public const string FirstName = "FirstName ";
        public const string LastName = "LastName ";
        public const string Email = "Email ";
        public const string Name = "Name ";
        public const string TotalScore = "TotalScore";
        public const string PlayerExists = "Player already exists.";
        public const string PlayerDoesNotExist = "Player does not exist.";
        public const string Get = "Get ";
        public const string Add = "Add ";
        public const string Remove = "Remove ";
        public const string Update = "Update ";
        public const string Id = "Id ";
        public const string PlayerId = "PlayerId ";
        public const string Player = "Player ";
        public const string Players = "Players ";
        public const string PlayerWithId = "Player with Id = "; //for player controller
        public const string PlayerWithPlayerId = "Player with PlayerId = "; //for player controller
        public const string DoesNotExist = " doesn't exist.";
        public const string GamesPlayed = "Gamesplayed ";
        public const string GamesPlayedForComparison = "Gamesplayed";
        public const string TotalScoreForComparison = "TotalScore";
        public const string And = "and ";
        public const string From = "from ";
        public const string In = "in ";
        public const string To = "to ";
        public const string For = "for ";
        public const string With = "with ";
        public const string LeaderBoard = "Leader Board ";
        public const string EmailFormatValidation = "Email must be of correct format. ";
        public const string AlphabetsValidation = "must be of correct format. The name must only contain alphabets."; 
        public const string MissingFNOrLN = "Both the attributes fn and ln is mandatory to fetch the details of the player without id. ";
        public const string InvalidGamesPlayedNumber = "must not be empty and must be greated than 0.";
        public const string Asc = "Asc";
        public const string OrderByValidation = "Both Orderby and Orderbykey are required.";

    }
}
