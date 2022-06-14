namespace EPharmacy.RESTApi.Controllers.v1;

public static partial class Routes
{
    public static class Common
    {
        private const string CommonRoute = Base + "/common";

        public static class Identity
        {
            private const string CommonIdentityRoute = CommonRoute + "/identity";

            public const string Logout = CommonIdentityRoute + "/logout";
        }

        public static class Users
        {
            private const string CommonUserRoute = CommonRoute + "/users";

            public const string Roles = CommonUserRoute + "/my-roles";
        }
    }
}