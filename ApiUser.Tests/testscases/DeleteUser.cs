using System;
using System.Linq;
using ApiUser.Controllers;
using ApiUser.Dtos;
using ApiUser.Models;
using ApiUser.Profiles;
using ApiUser.Repository;
using ApiUser.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ApiUser.testscases
{
    public class deleteUser
    {
        private readonly UserController userController;

        public deleteUser()
        {
            this.userController = new UserController(new UserService(), new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper());
        }


        [Fact]
        public void testDeleteExistingUser()
        {
            Assert.True(deleteUserVerification(1));
        }

        [Fact]
        public void testDeleteNonexistentUser()
        {
            Assert.False(deleteUserVerification(5));

        }

        [Fact]
        public void testDeleteNegativeId()
        {

            Assert.False(deleteUserVerification(-1));

        }

        

        private Boolean deleteUserVerification(int id)
        {
            try
            {

                int antes = userController.getAllUsers().Value.Count;

                userController.deleteUser(id);

                int depois = userController.getAllUsers().Value.Count;


                if (antes > depois)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                return false;
            }


        }



    }
}
