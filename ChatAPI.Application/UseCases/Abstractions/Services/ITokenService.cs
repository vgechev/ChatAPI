﻿using ChatAPI.Application.DTOs.Authorization;

namespace ChatAPI.Application.UseCases.Abstractions.Services
{
    public interface ITokenService
    {
        Task<TokensDTO> GenerateTokens(int userId);
        Task RevokeTokens(int userId);
        Task<bool> ValidateAccessToken(string accessToken);
        Task<bool> ValidateRefreshToken(string refreshToken);
    }
}