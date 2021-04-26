using System;
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
    public class FindUserById
    {
        private readonly UserController userController;

        public FindUserById()
        {
            this.userController = new UserController(new UserService(), new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper());

        }


        [Fact]
        public void testfindById_1()
        {

            UserReadDto user = userController.getUserById(1).Value;


            Assert.NotNull(user);
        }

        [Fact]
        public void testFindByNonexistentId()
        {
            Assert.False(findByIdUserVerification(4));
        }

        [Fact]
        public void testFindByIdNegative()
        {

            Assert.False(findByIdUserVerification(-4));
        }

          private Boolean findByIdUserVerification(int id)
        {

            try
            {
                UserReadDto user = userController.getUserById(id).Value;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }


    }
}
