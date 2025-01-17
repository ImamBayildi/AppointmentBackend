﻿using Core.Entities.Security;
using Core.Utilities.ResultPattern.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
