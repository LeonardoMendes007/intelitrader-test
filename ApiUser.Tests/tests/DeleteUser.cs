using System;
using System.Linq;
using ApiUser.Controllers;
using ApiUser.Dtos;
using ApiUser.Models;
using ApiUser.Profiles;
using ApiUser.Repository;
using ApiUser.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ApiUser.tests
{
    public class deleteUser
    {
        private readonly IUserController _userController;
        private readonly IUserService _userService;

        public deleteUser()
        {
            var mock = new Mock<ILogger<UserController>>();
            ILogger<UserController> logger = mock.Object;

            //or use this short equivalent 
            logger = Mock.Of<ILogger<UserController>>();

            this._userService = new UserServiceFake();
            this._userController = new UserController(_userService, new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper(), logger);

        }

        [Fact]
        public void deleteUser_WithId_1_ReturnsNotContent()
        {

            Assert.IsType<NoContentResult>(_userController.deleteUser(1));
            var okResult = _userService.findById(1);
            Assert.Null(okResult);
        }

        [Fact]
        public void deleteNonexistentUser_WithId_4_ReturnsBadRequest()
        {
            Assert.IsType<BadRequestResult>(_userController.deleteUser(4));
        }

        [Fact]
        public void deleteUser_WithNegativeId_ReturnsBadRequest()
        {
            Assert.IsType<BadRequestResult>(_userController.deleteUser(-1));
        }


    }
}
