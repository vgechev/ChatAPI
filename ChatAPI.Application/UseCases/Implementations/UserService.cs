﻿using System.Net;
using ChatAPI.Application.DTOs.Authorization;
using ChatAPI.Application.UseCases.Abstractions;
using ChatAPI.Application.Utilities;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.UseCases.Implementations
{
	public class UserService(IUserRepository userRepo) : IUserService
	{
		public async Task<Result> Register(UserRegisterDTO dto)
		{
			if (await userRepo.DoesUserExist(dto.Phone))
				return Result.Failure("This number is already in use.");

			userRepo.AddUserDevice(dto);
			await userRepo.SaveChangesAsync();
			return Result.Success(HttpStatusCode.Created);
		}

		// TODO with Twilio: send sms code to phone number

		/// <summary>
		/// Verifies the sms code of the user, generates a secret login code and returns it.
		/// </summary>
		/// <returns>A secret login code</returns>
		public async Task<Result<SecretLoginCodeDTO>> VerifySmsCode(VerifySmsCodeDTO dto)
		{
			// TODO with Twilio: send request to verify phone + code
			User? user = await userRepo.GetUserWithUserDevicesIncluded(dto.Phone);

			if (user is null)
				return Result<SecretLoginCodeDTO>.Failure("User with that phone is not found.", HttpStatusCode.NotFound);

			// The following is a custom implementation until we actually have Twilio
			if (dto.SmsVerificationCode != "000000")
				return Result<SecretLoginCodeDTO>.Failure("Wrong sms verification code.");

			Guid secretLoginCode = Guid.NewGuid();

			user.UserDevices ??= new List<UserDevice>();
			
			UserDevice? userDevice = user.UserDevices.FirstOrDefault(ud => ud.DeviceId == dto.DeviceId);

			if (userDevice is not null && userDevice.IsVerified)
				return Result<SecretLoginCodeDTO>.Failure("Device is already verified", HttpStatusCode.Forbidden);

			if (userDevice is null)
			{
				userDevice = new UserDevice(dto.DeviceId);
				user.UserDevices.Add(userDevice);
			}

			userDevice.IsVerified = true;
			userDevice.UserLoginCode = new(user.Id, secretLoginCode);
			await userRepo.SaveChangesAsync();

			return Result<SecretLoginCodeDTO>.Success(new(secretLoginCode));
		}
	}
}