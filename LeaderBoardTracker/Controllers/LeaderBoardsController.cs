using System;
using System.Collections.Generic;
using AutoMapper;
using LeaderBoardTracker.Common;
using LeaderBoardTracker.Data;
using LeaderBoardTracker.DTOs;
using LeaderBoardTracker.Interfaces;
using LeaderBoardTracker.Messages;
using LeaderBoardTracker.MockTest;
using LeaderBoardTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoardTracker.Controller
{
    //Controller to perform actions using Model LeaderBoard
    [Route("api/leaderboards")]
    [ApiController]
    public class LeaderBoardsController : ControllerBase
    {
        private readonly ILeaderBoardRepo _repository;
        private readonly IMapper _mapper;

        //private readonly MockLeaderBoardRepo _repository = new MockLeaderBoardRepo();

        public LeaderBoardsController(ILeaderBoardRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/leaderboards/
        [HttpGet]
        public ActionResult <List<ResponseLeaderBoardDTO>> GetAllLBEntries([FromQuery]string orderBy, [FromQuery] string orderByKey) 
        {
            List<ResponseLeaderBoardDTO> lstResponseDTO = new List<ResponseLeaderBoardDTO>();
            ResponseLeaderBoardDTO responseLeaderBoardDTO = new ResponseLeaderBoardDTO();
            if (orderBy != null)
            {
                orderBy = CommonFunctionalities.RemoveSpecialCharacters(orderBy);
            }

            if (orderByKey != null)
            {
                orderByKey = CommonFunctionalities.RemoveSpecialCharacters(orderByKey);
            }
            if ((String.IsNullOrEmpty(orderBy)) && (String.IsNullOrEmpty(orderByKey)))      //Checking for query string variables
            {
                var lbEntryItems = _repository.GetAllLBEntries();       // if null, access leaderboard entries by thier id
                lstResponseDTO = _mapper.Map<List<ResponseLeaderBoardDTO>>(lbEntryItems);
            }
            else if(((String.IsNullOrEmpty(orderBy)) && !(String.IsNullOrEmpty(orderByKey))) || (!(String.IsNullOrEmpty(orderBy)) && (String.IsNullOrEmpty(orderByKey)))) //Checking whether both the attributes orbey and orderbykey are mentioned
            {

                responseLeaderBoardDTO.ErrorMessage = Constants.OrderByValidation;
                lstResponseDTO.Add(responseLeaderBoardDTO);
            }
            else
            {
                if ((!String.IsNullOrEmpty(orderBy)) && (!String.IsNullOrEmpty(orderByKey)))
                {
                    var lbEntryItems = _repository.GetAllLBEntriesByOrder(orderBy, orderByKey);  //Getting the LB entries by order
                    lstResponseDTO = _mapper.Map<List<ResponseLeaderBoardDTO>>(lbEntryItems);
                   
                }
            }
            foreach (var item in lstResponseDTO)
            {
                item.Operation = Constants.Get + Constants.Players + Constants.From + Constants.LeaderBoard;
            }
            return Ok(lstResponseDTO);
        }

        //GET api/leaderboards/{id}
        [HttpGet("{id}", Name = "GetLBEntryById")]
        public ActionResult <ResponseLeaderBoardDTO> GetLBEntryById(int Id)
        {
            ResponseLeaderBoardDTO responseLeaderBoardDTO = new ResponseLeaderBoardDTO();
            string ErrMsg = Constants.PlayerWithId + Id + Constants.DoesNotExist;
            try
            {
                var playerItem = _repository.GetLBEntryById(Id); //Getting the LB entry by id
                if (playerItem != null)
                {
                    responseLeaderBoardDTO = _mapper.Map<ResponseLeaderBoardDTO>(playerItem);
                }
                else
                {
                    responseLeaderBoardDTO.StatusCode = 404;
                    responseLeaderBoardDTO.ErrorMessage = ErrMsg;
                }
            }
            catch(Exception ex)
            {
                responseLeaderBoardDTO.StatusCode = 404;
                responseLeaderBoardDTO.ErrorMessage = ex.Message.ToString();
            }
            responseLeaderBoardDTO.Operation = Constants.Get + Constants.Player + Constants.From + Constants.LeaderBoard + Constants.With + Constants.Id;
            return responseLeaderBoardDTO;
        }

        //GET api/leaderboards/playerid/{playerid}
        [Route("api/leaderboards/playerid/{playerid}")]
        [HttpGet("playerid/{playerid}", Name = "GetLBEntryByPlayerId")]
        public ActionResult <ResponseLeaderBoardDTO> GetLBEntryByPlayerId(int PlayerId)
        {
            ResponseLeaderBoardDTO responseLeaderBoardDTO = new ResponseLeaderBoardDTO();
            string ErrMsg = Constants.PlayerWithPlayerId + PlayerId + Constants.DoesNotExist;
            try
            {
                var playerItems = _repository.GetLBEntryByPlayerId(PlayerId); //Getting LB entries by Playerid
                if (playerItems != null)
                {
                    responseLeaderBoardDTO = _mapper.Map<ResponseLeaderBoardDTO>(playerItems);
                }
                else
                {
                    responseLeaderBoardDTO.PlayerId = PlayerId;
                    responseLeaderBoardDTO.StatusCode = 404;
                    responseLeaderBoardDTO.ErrorMessage = ErrMsg;
                }
            }
            catch(Exception ex)
            {
                responseLeaderBoardDTO.StatusCode = 404;
                responseLeaderBoardDTO.ErrorMessage = ex.Message.ToString();
            }
            responseLeaderBoardDTO.Operation = Constants.Get + Constants.Player + Constants.From + Constants.LeaderBoard + Constants.With + Constants.PlayerId;
            return responseLeaderBoardDTO;
        }

        //POST api/leaderboards
        [HttpPost]
        public ActionResult <ResponseLeaderBoardDTO> AddLBEntries(List<LeaderBoardDTO> lstAddLBEntries)
        {
            List<ResponseLeaderBoardDTO> lstResponse = new List<ResponseLeaderBoardDTO>();
            ResponseLeaderBoardDTO responseLeaderBoardDTO = new ResponseLeaderBoardDTO();
            foreach (var addLBitem in lstAddLBEntries)
            {
                try 
                {
                    responseLeaderBoardDTO = _mapper.Map<ResponseLeaderBoardDTO>(addLBitem);
                    var lbModelRepo = _repository.GetLBEntryByPlayerId(addLBitem.PlayerId);
                    if (lbModelRepo == null && addLBitem.GamesPlayed > 0)       //Checking whether the gamesplayed is not negative or 0 or even null
                    {
                        var LBModel = _mapper.Map<LeaderBoard>(addLBitem);
                        _repository.AddLBEntries(LBModel);
                        _repository.SaveChanges();


                        responseLeaderBoardDTO = _mapper.Map<ResponseLeaderBoardDTO>(LBModel);
                        responseLeaderBoardDTO.Operation = Constants.Add + Constants.Players + Constants.To + Constants.LeaderBoard;
                        responseLeaderBoardDTO.StatusCode = 200;
                    }
                    else
                    {
                        responseLeaderBoardDTO = _mapper.Map<ResponseLeaderBoardDTO>(addLBitem);
                        responseLeaderBoardDTO.Operation = Constants.Add + Constants.Players + Constants.To + Constants.LeaderBoard;
                        responseLeaderBoardDTO.StatusCode = 404;
                        if (addLBitem.GamesPlayed > 0)
                        {
                            responseLeaderBoardDTO.ErrorMessage = Constants.PlayerExists;
                        }
                        else
                        {
                            responseLeaderBoardDTO.ErrorMessage = Constants.GamesPlayed + Constants.And + Constants.InvalidGamesPlayedNumber;
                        }
                        
                    }
                }
                catch(Exception ex)
                {
                    responseLeaderBoardDTO.StatusCode = 404;
                    responseLeaderBoardDTO.ErrorMessage = ex.Message.ToString();
                }
                lstResponse.Add(responseLeaderBoardDTO);
            }
            return Ok(lstResponse);
        }

        //DELETE api/leaderboards
        [HttpDelete]
        public ActionResult <List<ResponseDeleteLBEntryDTO>> RemoveLBEntries(List<LeaderBoardDTO> lstDeleteLBEntries)
        {          
            List<ResponseDeleteLBEntryDTO> lstResponse = new List<ResponseDeleteLBEntryDTO>();
            ResponseDeleteLBEntryDTO responseDelLeaderBoardDTO = new ResponseDeleteLBEntryDTO();
            foreach (var deleteLBitem in lstDeleteLBEntries)
            {
                try
                {
                    var lbModelRepo = _repository.GetLBEntryByPlayerId(deleteLBitem.PlayerId);
                    if (lbModelRepo == null || deleteLBitem.GamesPlayed == 0) //Checking for mandatory Gamesplayed and player exists
                    {
                        responseDelLeaderBoardDTO = _mapper.Map<ResponseDeleteLBEntryDTO>(deleteLBitem);
                        responseDelLeaderBoardDTO.Operation = Constants.Remove + Constants.Players + Constants.From + Constants.LeaderBoard;
                        responseDelLeaderBoardDTO.StatusCode = 404;
                        if (deleteLBitem.GamesPlayed != 0 && lbModelRepo == null)
                        {
                            responseDelLeaderBoardDTO.ErrorMessage = Constants.PlayerDoesNotExist;
                        }
                        else
                        {
                            responseDelLeaderBoardDTO.ErrorMessage = Constants.GamesPlayed + Constants.MandatoryField + Constants.InvalidGamesPlayedNumber;
                        }
                    }
                    else
                    {
                        var LBModel = _mapper.Map<LeaderBoard>(lbModelRepo);
                        Player playerDeatils = _repository.GetPlayerInfo(deleteLBitem.PlayerId);
                        _repository.RemoveLBEntries(LBModel);
                        _repository.SaveChanges();

                        responseDelLeaderBoardDTO = _mapper.Map<ResponseDeleteLBEntryDTO>(LBModel);
                        responseDelLeaderBoardDTO.FirstName = playerDeatils.FirstName;
                        responseDelLeaderBoardDTO.LastName = playerDeatils.LastName;
                        responseDelLeaderBoardDTO.Operation = Constants.Remove + Constants.Players + Constants.From + Constants.LeaderBoard;
                        responseDelLeaderBoardDTO.StatusCode = 200;
                    }
                }             
                catch(Exception ex)
                {
                    responseDelLeaderBoardDTO.StatusCode = 404;
                    responseDelLeaderBoardDTO.ErrorMessage = ex.Message.ToString();
                }
                lstResponse.Add(responseDelLeaderBoardDTO);
            }
                return lstResponse;
        }

        //PUT api/leaderboards
        [HttpPut]
        public ActionResult <List<ResponseLeaderBoardDTO>> UpdateLBEntries(List<LeaderBoardDTO> lstUpdateLBEntries)
        {
            List<ResponseLeaderBoardDTO> lstResponse = new List<ResponseLeaderBoardDTO>();
            ResponseLeaderBoardDTO responseLeaderBoardDTO = new ResponseLeaderBoardDTO();
            foreach (var updateLBitem in lstUpdateLBEntries)
            {
                try
                {
                    var lbModelRepo = _repository.GetLBEntryByPlayerId(updateLBitem.PlayerId); 
                    if (lbModelRepo == null || updateLBitem.GamesPlayed == 0)
                    {
                        responseLeaderBoardDTO = _mapper.Map<ResponseLeaderBoardDTO>(updateLBitem);
                        responseLeaderBoardDTO.Operation = Constants.Update + Constants.Players + Constants.In + Constants.LeaderBoard;
                        responseLeaderBoardDTO.StatusCode = 404;
                        if (updateLBitem.GamesPlayed != 0 && lbModelRepo == null)
                        {
                            responseLeaderBoardDTO.ErrorMessage = Constants.PlayerDoesNotExist;
                        }
                        else
                        {
                            responseLeaderBoardDTO.ErrorMessage = Constants.GamesPlayed + Constants.MandatoryField + Constants.InvalidGamesPlayedNumber;
                        }
                    }
                    else
                    {
                        var LBModel = _mapper.Map<LeaderBoard>(updateLBitem);
                        _mapper.Map(updateLBitem, lbModelRepo);
                        _repository.UpdateLBEntries(lbModelRepo);
                        _repository.SaveChanges();
                        responseLeaderBoardDTO = _mapper.Map<ResponseLeaderBoardDTO>(LBModel);
                        responseLeaderBoardDTO.Operation = Constants.Update + Constants.Players + Constants.From + Constants.LeaderBoard;
                        responseLeaderBoardDTO.StatusCode = 200;
                    }
                }
                catch (Exception ex)
                {
                    responseLeaderBoardDTO.StatusCode = 404;
                    responseLeaderBoardDTO.ErrorMessage = ex.Message.ToString();
                }
                lstResponse.Add(responseLeaderBoardDTO);
            }
            return lstResponse;
        }
    }
}