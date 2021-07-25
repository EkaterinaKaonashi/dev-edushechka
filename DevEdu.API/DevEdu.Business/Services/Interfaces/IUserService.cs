﻿using DevEdu.DAL.Models;
using System.Collections.Generic;

namespace DevEdu.Business.Services
{
    public interface IUserService
    {
        int AddUser(UserDto dto, IAuthenticationService authenticationService);
        void AddUserRole(int userId, int roleId);
        void DeleteUser(int id);
        void DeleteUserRole(int userId, int roleId);
        UserDto SelectUserById(int id);
        public UserDto SelectUserByEmail(string email);
        List<UserDto> SelectUsers();
        UserDto UpdateUser(UserDto dto);
    }
}