using AutoMapper;
using LeaderBoardTracker.DTOs;
using LeaderBoardTracker.Models;

namespace LeaderBoardTracker.Profiles
{
    public class LeaderBoardTrackerProfile : Profile
    {
        public LeaderBoardTrackerProfile()
        {
            //Source -> target
            CreateMap<AddOrUpdatePlayerDTO, Player>();
            CreateMap<Player, ResponsePlayerDTOToRead>();
            CreateMap<Player, ResponsePlayerDTO>();
            CreateMap<AddOrUpdatePlayerDTO, ResponsePlayerDTO>();
            CreateMap<Player, ResponseDeletePlayerDTO>();
            CreateMap<LeaderBoard, ResponseDeleteLBEntryDTO>();
            CreateMap<LeaderBoardDTO, ResponseDeleteLBEntryDTO>();
            CreateMap<LeaderBoardDTO, LeaderBoard>();
            CreateMap<LeaderBoard, ResponseLeaderBoardDTO>();
            CreateMap<LeaderBoardDTO, ResponseLeaderBoardDTO>();
        }
    }
}