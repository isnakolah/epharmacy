namespace EPharmacy.RESTApi.Controllers.v1;

public static partial class Routes
{
    public static class System
    {
        private const string SystemRoute = $"{Base}/system";

        public static class Identity
        {
            private const string SystemIdentityRoute = $"{SystemRoute}{IdentityRoute}";

            public const string Login = SystemIdentityRoute + "/login";

            public const string Logout = SystemIdentityRoute + "/logout";
        }

        public static class Users
        {
            private const string SysteUserRoute = SystemRoute + "/users";

            public const string Create = SysteUserRoute;
        }
    }
}
