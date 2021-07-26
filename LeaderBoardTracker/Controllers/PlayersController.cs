using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using LeaderBoardTracker.Common;
using LeaderBoardTracker.Data;
using LeaderBoardTracker.DTOs;
using LeaderBoardTracker.Interfaces;
using LeaderBoardTracker.Messages;
using LeaderBoardTracker.MockTest;
using LeaderBoardTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoardTracker.Controllers
{
    //Controller to perform actions using Model Player
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepo _repository;
        private readonly IMapper _mapper;

        //private readonly MockPlayerRepo _repository = new MockPlayerRepo();

        public PlayersController(IPlayerRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/players/
        [HttpGet]
        public ActionResult<List<ResponsePlayerDTOToRead>> GetAllPlayers()
        {
            var playerItems = _repository.GetAllPlayers();
            List<ResponsePlayerDTOToRead> lstResponseDTO = new List<ResponsePlayerDTOToRead>();
            lstResponseDTO = _mapper.Map<List<ResponsePlayerDTOToRead>>(playerItems);
            foreach (var item in lstResponseDTO)
            {
                item.Operation = Constants.Get + Constants.Player;
            }
            return lstResponseDTO;
        }

        //GET api/players/{id}/player
        [Route("api/players/{id}/player")]
        [HttpGet("{id}/player", Name = "GetPlayerById")]
        public ActionResult<ResponsePlayerDTO> GetPlayerById(int Id)
        {
            ResponsePlayerDTO responsePlayerDTO = new ResponsePlayerDTO();
            List<string> ErrorMsgs = new List<string>();
            string ErrMsg = Constants.PlayerDoesNotExist;
            try
            {
                var playerItems = _repository.GetPlayerById(Id);
                if (playerItems != null)
                {
                    responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(playerItems);
                }
                else
                {
                    responsePlayerDTO.Id = Id;
                    ErrorMsgs.Add(ErrMsg);
                    responsePlayerDTO.ErrorMessage = ErrorMsgs;
                }
            }
            catch (Exception ex)
            {
                responsePlayerDTO.Id = Id;
                ErrorMsgs.Add(ex.Message.ToString());
                responsePlayerDTO.ErrorMessage = ErrorMsgs;
            }
            responsePlayerDTO.Operation = Constants.Get + Constants.Player;
            return responsePlayerDTO;
        }

        //GET api/players/byName?fn="a"&ln="b"
        [Route("api/players/byName")]
        [HttpGet("{byName}", Name = "GetPlayerByName")]
        public ActionResult<ResponsePlayerDTO> GetPlayerByName([FromQuery]string fn, [FromQuery]string ln) //fn => firstname and ln = lastname
        {
            ResponsePlayerDTO responsePlayerDTO = new ResponsePlayerDTO();
            List<string> ErrorMsgs = new List<string>();
            string ErrMsg = String.Empty;
            try
            {
                //Checking whether the querystring parameters are null
                if (fn != null && ln != null)
                {
                    fn = CommonFunctionalities.RemoveSpecialCharacters(fn); // removing special characters from the string
                    ln = CommonFunctionalities.RemoveSpecialCharacters(ln);

                    var playerItems = _repository.GetPlayerByName(fn, ln);  //Getting player info by name
                    if (playerItems != null)
                    {
                        responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(playerItems);
                    }
                }
                else
                {
                    if (fn == null)
                    {
                        responsePlayerDTO.FirstName = fn;   // assigning the final response the values from the query string
                    }
                    else
                    {
                        responsePlayerDTO.FirstName = CommonFunctionalities.RemoveSpecialCharacters(fn);
                    }

                    if (ln == null)
                    {
                        responsePlayerDTO.LastName = ln;
                    }
                    else
                    {
                        responsePlayerDTO.LastName = CommonFunctionalities.RemoveSpecialCharacters(ln);
                    }

                    if (fn != null && ln != null) // if the fn and ln parameters are not null and their specified entry doesn't exist, then adding the error message "Player doesn't exist"
                    {
                        ErrMsg = Constants.PlayerDoesNotExist;
                    }
                    else
                    {
                        ErrMsg = Constants.MissingFNOrLN;
                    }
                    ErrorMsgs.Add(ErrMsg);
                    responsePlayerDTO.ErrorMessage = ErrorMsgs;
                }
            }
            catch (Exception ex)
            {
                ErrorMsgs.Add(ex.Message.ToString());
                responsePlayerDTO.ErrorMessage = ErrorMsgs;
            }
            responsePlayerDTO.Operation = Constants.Get + Constants.Player;
            return responsePlayerDTO;
        }

        //POST api/players
        [HttpPost]
        public ActionResult<ResponsePlayerDTO> AddPlayer(AddOrUpdatePlayerDTO addPlayerDTO)
        {
            ResponsePlayerDTO responsePlayerDTO = new ResponsePlayerDTO();
            List<string> ErrorMsgs = new List<string>();
            string missingErrMsg = String.Empty;
            try
            {
                //Checking for null or Empty
                if ((String.IsNullOrEmpty(addPlayerDTO.FirstName)) || (String.IsNullOrEmpty(addPlayerDTO.LastName)) || (String.IsNullOrEmpty(addPlayerDTO.Email))
                    || (String.IsNullOrWhiteSpace(addPlayerDTO.FirstName)) || (String.IsNullOrWhiteSpace(addPlayerDTO.LastName)) || (String.IsNullOrWhiteSpace(addPlayerDTO.Email))
                    || (!CommonFunctionalities.Validate(Constants.Email, addPlayerDTO.Email)) || (!CommonFunctionalities.Validate(Constants.Name, addPlayerDTO.FirstName)) || (!CommonFunctionalities.Validate(Constants.Name, addPlayerDTO.LastName)))
                {
                    if ((String.IsNullOrEmpty(addPlayerDTO.FirstName)) || (String.IsNullOrWhiteSpace(addPlayerDTO.FirstName)))  //Checking for FirstName mandatoryfield
                    {
                        responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(addPlayerDTO);
                        missingErrMsg = Constants.FirstName + Constants.MandatoryField;
                        responsePlayerDTO.StatusCode = 400;
                        ErrorMsgs.Add(missingErrMsg);
                    }
                    else
                    {
                        //Checking whether the name consists of only letter 
                        if (!CommonFunctionalities.Validate(Constants.Name, addPlayerDTO.FirstName))       //Checking whether the name contains all alphabets
                        {
                            responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(addPlayerDTO);
                            missingErrMsg = Constants.FirstName + Constants.AlphabetsValidation;
                            responsePlayerDTO.StatusCode = 400;
                            ErrorMsgs.Add(missingErrMsg);
                        }
                    }
                    if ((String.IsNullOrEmpty(addPlayerDTO.LastName)) || (String.IsNullOrWhiteSpace(addPlayerDTO.LastName)))    //Checking for LastName mandatoryfield
                    {
                        responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(addPlayerDTO);
                        missingErrMsg = Constants.LastName + Constants.MandatoryField;
                        responsePlayerDTO.StatusCode = 400;
                        ErrorMsgs.Add(missingErrMsg);
                    }
                    else
                    {
                        //Checking whether the name consists of only letter
                        if (!CommonFunctionalities.Validate(Constants.Name, addPlayerDTO.LastName))       //Checking whether the name contains all alphabets
                        {
                            responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(addPlayerDTO);
                            missingErrMsg = Constants.LastName + Constants.AlphabetsValidation;
                            responsePlayerDTO.StatusCode = 400;
                            ErrorMsgs.Add(missingErrMsg);
                        }
                    }
                    if ((String.IsNullOrEmpty(addPlayerDTO.Email)) || (String.IsNullOrWhiteSpace(addPlayerDTO.Email)))  //Checking for EmailName mandatoryfield
                    {
                        responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(addPlayerDTO);
                        missingErrMsg = Constants.Email + Constants.MandatoryField;
                        responsePlayerDTO.StatusCode = 400;
                        ErrorMsgs.Add(missingErrMsg);
                    }
                    else
                    {
                        if (!CommonFunctionalities.Validate(Constants.Email, addPlayerDTO.Email))     //Checking whether the email is in the right format
                        {
                            responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(addPlayerDTO);
                            missingErrMsg = Constants.EmailFormatValidation;
                            responsePlayerDTO.StatusCode = 400;
                            ErrorMsgs.Add(missingErrMsg);
                        }
                    }
                }
                else
                {
                    //Checking whether the player exists by names as the id does not exist yet
                    var playerModelRepo = _repository.GetPlayerByName(addPlayerDTO.FirstName, addPlayerDTO.LastName);
                    if ((playerModelRepo != null) && ((playerModelRepo.FirstName == addPlayerDTO.FirstName) && (playerModelRepo.LastName == addPlayerDTO.LastName) && (playerModelRepo.Email == addPlayerDTO.Email)))
                    {
                        responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(addPlayerDTO);
                        missingErrMsg = Constants.PlayerExists;
                        responsePlayerDTO.StatusCode = 400;
                        ErrorMsgs.Add(missingErrMsg);
                    }
                    else
                    {
                        //if all the validations pass then the player will be added from the below function
                        var playerModel = _mapper.Map<Player>(addPlayerDTO);
                        _repository.AddPlayer(playerModel);
                        _repository.SaveChanges();

                        responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(playerModel);
                        responsePlayerDTO.StatusCode = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                missingErrMsg = ex.Message.ToString();
                ErrorMsgs.Add(missingErrMsg);
            }
            responsePlayerDTO.Operation = Constants.Add + Constants.Player;
            responsePlayerDTO.ErrorMessage = ErrorMsgs;
            return responsePlayerDTO;
        }

        //DELETE api/players/{id}
        [HttpDelete("{id}")]
        public ActionResult<ResponseDeletePlayerDTO> RemovePlayer(int id)
        {
            ResponseDeletePlayerDTO responseDeletePlayerDTO = new ResponseDeletePlayerDTO();
            List<string> ErrorMsgs = new List<string>();
            string missingErrMsg = String.Empty;
            try
            {
                var playerModelFromRepo = _repository.GetPlayerById(id); //fetching the player details to check whether the player exists
                if (playerModelFromRepo == null)
                {
                    missingErrMsg = Constants.PlayerWithId + id + Constants.DoesNotExist;
                    responseDeletePlayerDTO.Id = id;
                    responseDeletePlayerDTO.StatusCode = 400;
                    ErrorMsgs.Add(missingErrMsg);
                }
                else
                {
                    _repository.RemovePlayer(playerModelFromRepo);
                    _repository.SaveChanges();

                    responseDeletePlayerDTO = _mapper.Map<ResponseDeletePlayerDTO>(playerModelFromRepo);
                    responseDeletePlayerDTO.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                missingErrMsg = ex.Message.ToString();
                ErrorMsgs.Add(missingErrMsg);
            }
            responseDeletePlayerDTO.Operation = Constants.Remove + Constants.Player;
            responseDeletePlayerDTO.ErrorMessage = ErrorMsgs;
            return responseDeletePlayerDTO;
        }

        //PUT api/players/{id}
        [HttpPut("{id}")]
        public ActionResult<ResponsePlayerDTO> UpdatePlayer(int id, AddOrUpdatePlayerDTO updatePlayerDTO)
        {
            ResponsePlayerDTO responsePlayerDTO = new ResponsePlayerDTO();
            List<string> ErrorMsgs = new List<string>();
            string missingErrMsg = String.Empty;
            try
            {
                //Checking whether all the required attributes are present
                if ((String.IsNullOrEmpty(updatePlayerDTO.FirstName)) || (String.IsNullOrEmpty(updatePlayerDTO.LastName)) || (String.IsNullOrEmpty(updatePlayerDTO.Email)) || (String.IsNullOrWhiteSpace(updatePlayerDTO.FirstName)) || (String.IsNullOrWhiteSpace(updatePlayerDTO.LastName)) || (String.IsNullOrWhiteSpace(updatePlayerDTO.Email)))
                {
                    responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(updatePlayerDTO);
                    responsePlayerDTO.StatusCode = 400;
                    responsePlayerDTO.Id = id;
                    if ((String.IsNullOrEmpty(updatePlayerDTO.FirstName)) || (String.IsNullOrWhiteSpace(updatePlayerDTO.FirstName)))
                    {
                        missingErrMsg = Constants.FirstName + Constants.MandatoryField;
                        ErrorMsgs.Add(missingErrMsg);
                    }
                    if ((String.IsNullOrEmpty(updatePlayerDTO.LastName)) || (String.IsNullOrWhiteSpace(updatePlayerDTO.LastName)))
                    {
                        missingErrMsg = Constants.LastName + Constants.MandatoryField;
                        ErrorMsgs.Add(missingErrMsg);
                    }
                    if ((String.IsNullOrEmpty(updatePlayerDTO.Email)) || (String.IsNullOrWhiteSpace(updatePlayerDTO.Email)))
                    {
                        missingErrMsg = Constants.Email + Constants.MandatoryField;
                        ErrorMsgs.Add(missingErrMsg);
                    }
                }
                else
                {
                    var playerModelFromRepo = _repository.GetPlayerById(id);
                    if (playerModelFromRepo == null)        //Checking whether the player exists
                    {
                        missingErrMsg = Constants.PlayerWithId + id + Constants.DoesNotExist;
                        responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(updatePlayerDTO);
                        responsePlayerDTO.Id = id;
                        responsePlayerDTO.StatusCode = 400;
                        ErrorMsgs.Add(missingErrMsg);
                    }
                    else
                    {
                        _mapper.Map(updatePlayerDTO, playerModelFromRepo);
                        _repository.UpdatePlayer(playerModelFromRepo);          //Updating the player details if the input passes all the validations
                        _repository.SaveChanges();
                        responsePlayerDTO = _mapper.Map<ResponsePlayerDTO>(playerModelFromRepo);
                        responsePlayerDTO.StatusCode = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                missingErrMsg = ex.Message.ToString();
                ErrorMsgs.Add(missingErrMsg);
            }
            responsePlayerDTO.Operation = Constants.Update + Constants.Player;
            responsePlayerDTO.ErrorMessage = ErrorMsgs;
            return responsePlayerDTO;
        }
    }
}