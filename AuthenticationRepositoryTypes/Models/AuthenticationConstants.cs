namespace AuthenticationRepositoryTypes
{
    public static class AuthenticationConstants
    {
        public const string OrganizationAPIKey = "test";

        public const string AuthenticationAdminOrganizationName = "Organization Admin";

        public const string AuthenticationAdminDefaultUserName = "admin";
        public const string AuthenticationAdminDefaultPassword = "password";
        public const string AuthenticationAdminDefaultEmail = "admin@multisweeps.com";

        public const string REQUIRED_ISSUER = @"EBC35473-582C-4139-9CF8-5C52EDF37372";

        public const string POLICY_ADMIN = "5150";

        public const string POLICY_USER = $"2001, {POLICY_ADMIN}";

        public const string POLICY_RESELLER = "3001";

        public const string POLICY_ALL = $"{POLICY_USER}, {POLICY_RESELLER}";
    }
}
