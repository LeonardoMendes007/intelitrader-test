using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using ApiUser.Models;
using Microsoft.AspNetCore.Mvc;
using ApiUser.Services;
using AutoMapper;
using ApiUser.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using ApiUser.Exceptions;

namespace ApiUser.Controllers
{

    [Route("Users")]
    [ApiController]
    public class UserController : ControllerBase, IUserController
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<UserReadDto>> getAllUsers()
        {

            List<User> users = _userService.findAll();

            return _mapper.Map<List<UserReadDto>>(users);

        }

        [HttpGet("{id}", Name = "getUserById")]
        public ActionResult<UserReadDto> getUserById(int id)
        {

            User user = _userService.findById(id);
            if (user == null)
            {
                return NotFound(CustomErrors.NotFound($"There is no resource with id = {id}", Request));
            }
            return _mapper.Map<UserReadDto>(user);

        }

        [HttpPost]
        public ActionResult<UserReadDto> createUser(UserCreateDto userDto)
        {

            User userModelFromRepo = _mapper.Map<User>(userDto);

            if (userDto == null)
            {
                return BadRequest(CustomErrors.BadRequest($"transaction not successful", Request));
            }

            _userService.save(userModelFromRepo);

            try
            {
            }
            catch (Exception e)
            {
                return BadRequest(CustomErrors.InternalServerError($"A server error has occurred, if it persists, please try again later.", Request));
            }

            UserReadDto userReadDto = _mapper.Map<UserReadDto>(userModelFromRepo);

            return CreatedAtRoute(nameof(getUserById), new { Id = userReadDto.Id }, userReadDto);



        }

        [HttpPut("{id}")]
        public ActionResult updateUser(int id, UserUpdateDto userDto)
        {

            User userModelFromRepo = _userService.findById(id);

            if (userModelFromRepo == null)
            {
                return NotFound(CustomErrors.NotFound($"Resource with id = {id}", Request));
            }

            _mapper.Map(userDto, userModelFromRepo);

            _userService.update(userModelFromRepo);

            try
            {
            }
            catch (Exception e)
            {
                return BadRequest(CustomErrors.InternalServerError($"A server error has occurred, if it persists, please try again later.", Request));
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult deleteUser(int id)
        {

            User userModelFromRepo = _userService.findById(id);
            if (userModelFromRepo == null)
            {
                return BadRequest(CustomErrors.BadRequest($"Resource with id = {id}", Request));
            }
            _userService.delete(userModelFromRepo);
            try
            {
            }
            catch (Exception e)
            {
                return BadRequest(CustomErrors.InternalServerError($"A server error has occurred, if it persists, please try again later.", Request));
            }


            return NoContent();


        }


    }
}