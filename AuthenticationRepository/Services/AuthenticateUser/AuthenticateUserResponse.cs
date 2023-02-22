﻿namespace AuthenticationRepository
{
    using DatabaseContext;

    public class AuthenticateUserResponse
    {
        public bool IsSuccessful { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Claims { get; set; }

        public List<int> Roles { get; set; } = new List<int>();
    }
}
