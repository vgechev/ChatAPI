﻿using ChatAPI.Application.DTOs.Authorization;
using ChatAPI.Application.Utilities;

namespace ChatAPI.Application.UseCases.Abstractions
{
    public interface IUserService
	{
		Task<Result> Register(UserRegisterDTO dto);
		Task<Result<SecretLoginCodeDTO>> VerifySmsCode(VerifySmsCodeDTO code);
	}
}
