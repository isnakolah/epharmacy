namespace EPharmacy.RESTApi.Controllers.v1;

public static partial class Routes
{
    public static class Provider
    {
        private const string PharmacyRoute = Base + "/pharmacy";

        public static class Identity
        {
            private const string PharmacyIdentityRoute = PharmacyRoute + IdentityRoute;

            public const string Login = PharmacyIdentityRoute + "/login";

            public const string Logout = PharmacyIdentityRoute + "/logout";

            public const string SendPasswordResetEmail = PharmacyIdentityRoute + "/send-reset-password-email";

            public const string UpdatePassword = PharmacyIdentityRoute + "/change-password";

            public const string CreatePassword = $"{PharmacyIdentityRoute}/create-password";

            public const string ConfirmEmail = PharmacyIdentityRoute + "/confirm-email";

        }

        public static class Users
        {
            private const string PharmacyUsersRoute = PharmacyRoute + "/users";

            public const string Create = PharmacyUsersRoute;

            public const string Update = PharmacyUsersRoute + "/{userID:guid}";

            public const string GetAll = PharmacyUsersRoute;

            public const string GetSingleByID = PharmacyUsersRoute + "/{id:guid}";

            public const string Delete = PharmacyUsersRoute + "/{id:guid}";

            public const string MakeAdmin = PharmacyUsersRoute + "/make-admin/{id:guid}";
        }

        public static class Prescription
        {
            private const string PharmacyPrescriptionRoute = PharmacyRoute + "/prescription";

            public const string GetAll = PharmacyPrescriptionRoute;
        }

        public static class Quotation
        {
            private const string PharmacyQuotationRoute = PharmacyRoute + "/quotation";

            public const string Create = PharmacyQuotationRoute;

            public const string GetAll = PharmacyQuotationRoute;

            public const string GetSingleByID = PharmacyQuotationRoute + "/{id:guid}";
        }

        public static class WorkOrder
        {
            private const string PharmacyWorkOrderRoute = PharmacyRoute + "/workorder";

            public const string GetAll = PharmacyWorkOrderRoute;

            public const string Dispatch = PharmacyWorkOrderRoute + "/dispatch/{id:guid}";
        }

        public static class Dashboard
        {
            private const string PharmacyDashboardRoute = PharmacyRoute + "/dashboard";

            public const string Summary = PharmacyDashboardRoute + "/summary";
        }

        public static class ServiceCatalogue
        {
            private const string PharmacyServiceCatalogueRoute = $"{PharmacyRoute}/service-catalogue";

            public const string GetAll = $"{PharmacyServiceCatalogueRoute}";

            public const string ToggleStocked = $"{PharmacyServiceCatalogueRoute}/toggle-stocked/{{serviceID:long}}";
        }
    }
}