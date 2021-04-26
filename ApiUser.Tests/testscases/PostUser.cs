using System;
using System.Linq;
using ApiUser.Controllers;
using ApiUser.Dtos;
using ApiUser.Models;
using ApiUser.Profiles;
using ApiUser.Services;
using AutoMapper;
using Xunit;

namespace ApiUser.testscases
{
    public class PostUsers
    {
        private readonly UserController userController;

        public PostUsers()
        {
            this.userController = new UserController(new UserService(), new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper());

        }

        [Fact]
        public void testPostValidUser()
        {

            UserCreateDto user = new UserCreateDto("Gustavo", "Mendes", 19);

            Assert.True(postUserVerification(user));
        }

        [Fact]
        public void testPostNullUser()
        {

            UserCreateDto user = null;

            Assert.False(postUserVerification(user));
        }

        [Fact]
        public void testPostUserWithNullSurname()
        {

            UserCreateDto user = new UserCreateDto("Gustavo", null, 19);

            Assert.True(postUserVerification(user));
        }

        [Fact]
        public void testPostUserWithNullName()
        {

            UserCreateDto user = new UserCreateDto(null, "Mendes", 19);

            Assert.False(postUserVerification(user));
        }

        [Fact]
        public void testPostUserWithNullAge()
        {

            UserCreateDto user = new UserCreateDto("Gustavo", "Mendes", 0);

            Assert.False(postUserVerification(user));
        }



        private Boolean postUserVerification(UserCreateDto user)
        {
            try
            {

                UserReadDto persistenceUser = userController.createUser(user).Value;

                if (persistenceUser.Name.Equals(user.Name) && persistenceUser.Age == user.Age)
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
