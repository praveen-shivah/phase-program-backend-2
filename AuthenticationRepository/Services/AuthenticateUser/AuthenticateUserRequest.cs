﻿namespace AuthenticationRepository
{
    public class AuthenticateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }
}