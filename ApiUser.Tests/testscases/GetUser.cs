
using System.Collections.Generic;
using ApiUser.Controllers;
using ApiUser.Dtos;
using ApiUser.Models;
using ApiUser.Profiles;
using ApiUser.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiUser.testscases
{
    public class GetUser
    {
        private readonly UserController userController;

        public GetUser()
        {
            this.userController = new UserController(new UserService(), new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper());
        }

        [Fact]
        public void testReturnNull()
        {

            Assert.NotNull(userController.getAllUsers());
        }

        [Fact]
        public void testReturnEmpty()
        {

            List<UserReadDto> users = userController.getAllUsers().Value;

            int count = users.Count;

            bool ver = false;

            if (count == 3)
            {
                ver = true;
            }

            Assert.True(ver);
        }

        


    }
}
