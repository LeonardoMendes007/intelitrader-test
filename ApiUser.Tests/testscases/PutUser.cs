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
    public class PutUser
    {
        private readonly UserController userController;

        public PutUser()
        {
            this.userController = new UserController(new UserService(), new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper());

        }

        [Fact]
        public void testPutValidUser()
        {

            UserUpdateDto user = new UserUpdateDto("Leon", "Mend", 21);


            Assert.True(putUserVerification(1, user));
        }

        [Fact]
        public void testPutNullUser()
        {

            UserUpdateDto user = null;


            Assert.False(putUserVerification(1, user));
        }

        [Fact]
        public void testPutUserWithNullSurname()
        {
            UserUpdateDto user = new UserUpdateDto("Leon", null, 21);

            Assert.True(putUserVerification(1, user));

        }
        


        private Boolean putUserVerification(int id, UserUpdateDto user)
        {
            try
            {

                userController.updateUser(id, user);

                UserReadDto persistenceUser = userController.getUserById(id).Value;

                if (persistenceUser.Name.Equals(user.Name) && persistenceUser.Surname == user.Surname && persistenceUser.Age == user.Age)
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
