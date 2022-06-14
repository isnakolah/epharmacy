namespace EPharmacy.RESTApi.Controllers.v1;

public static partial class Routes
{
    public static class Ponea
    {
        private const string PoneaRoute = Base + "/ponea";

        public static class Identity
        {
            private const string PoneaIdentityRoute = PoneaRoute + IdentityRoute;

            public const string Login = PoneaIdentityRoute + "/login";

            public const string Logout = PoneaIdentityRoute + "/logout";
        }

        public static class Users
        {
            private const string PoneaUsersRoute = PoneaRoute + "/users";

            public const string Create = PoneaUsersRoute;

            public const string GetAll = PoneaUsersRoute;

            public const string GetSingle = PoneaUsersRoute + "/{pharmacyAdminID:guid}";

            public const string Update = PoneaUsersRoute + "/{pharmacyAdminID:guid}";

            public const string Delete = PoneaUsersRoute + "/{pharmacyAdminID:guid}";
        }

        public static class Pharmacy
        {
            private const string PoneaPharmacyRoute = PoneaRoute + "/pharmacy";

            public const string GetAll = PoneaPharmacyRoute;

            public const string GetSingleByID = PoneaPharmacyRoute + "/{id:guid}";

            public const string SearchByName = PoneaPharmacyRoute + "/search";

            public const string GetAllPharmacyIDs = PoneaPharmacyRoute + "/ids";
        }

        public static class Prescription
        {
            private const string PoneaPrescriptionRoute = PoneaRoute + "/prescription";

            public const string Create = PoneaPrescriptionRoute;

            public const string GetAll = PoneaPrescriptionRoute;

            public const string GetSingleByID = PoneaPrescriptionRoute + "/{id:guid}";

            public const string Cancel = PoneaPrescriptionRoute + "/cancel/{id:guid}";

            public const string SearchDrug = PoneaPrescriptionRoute + "/drug/search/";
        }

        public static class Quotation
        {
            private const string PoneaQuotationRoute = PoneaRoute + "/quotation";

            public const string GetAll = PoneaQuotationRoute;

            public const string GetSingleByID = PoneaQuotationRoute + "/{id:guid}";

            public const string Approve = PoneaQuotationRoute + "/approve/{id:guid}";

            public const string Reject = PoneaQuotationRoute + "/reject/{id:guid}";
        }

        public static class WorkOrder
        {
            private const string PoneaWorkOrderRoute = PoneaRoute + "/workorder";

            public const string Create = PoneaWorkOrderRoute;
        }

        public static class Category
        {
            private const string PoneaCategoryRoute = PoneaRoute + "/category";

            public const string GetAll = PoneaCategoryRoute;

            public const string Create = PoneaCategoryRoute;
        }

        public static class Formulation
        {
            private const string PoneaFormulationRoute = PoneaRoute + "/formulation";

            public const string GetAll = PoneaFormulationRoute;

            public const string Create = PoneaFormulationRoute;
        }
    }
}