﻿namespace AuthenticationRepository
{
    using ApiDTO;

    using DatabaseContext;

    public class RefreshTokenResponse
    {
        public bool IsSuccessful { get; set; }

        public RefreshTokenResponseType RefreshTokenResponseType { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public RefreshTokenDto RefreshToken { get; set; }

        public string JwtToken { get; set; }
    }
}
