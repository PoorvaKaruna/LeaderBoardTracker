using LeaderBoardTracker.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeaderBoardTracker.Common
{
    public class CommonFunctionalities
    {
        //Class containing all the common functions
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
        }

        public static bool Validate(string validatefor, string str)
        {
            bool isMatch = false;
            Match match;
            switch (validatefor)
            {
                case Constants.Email:
                    Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    match = regexEmail.Match(str);
                    if (match.Success)
                        isMatch = true;
                    break;
                case Constants.Name:
                    Regex regexName = new Regex(@"^[a-zA-Z]+$");
                    match = regexName.Match(str);
                    if (match.Success)
                        isMatch = true;
                    break;
            }
            return isMatch;
        }
    }
}
