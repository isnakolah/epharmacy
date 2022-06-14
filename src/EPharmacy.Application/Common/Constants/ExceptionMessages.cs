namespace EPharmacy.Application.Common.Constants;

public static class ExceptionMessages
{
    public static class Prescription
    {
        public const string Cancelled = "Prescription is already cancelled";

        public const string AlreadyApproved = "Prescription is already approved";

        public const string AlreadyQuoted = "This prescription has already been quoted";

        public const string Expired = "This prescription has expired";
    }

    public static class Quotation
    {
        public const string Cancelled = "Quotation has been cancelled";

        public const string AlreadyApproved = "Quotation has already been approved";

        public const string NeedsApproval = "Quotation has to be approved in order to create a workorder";
    }

    public static class WorkOrder
    {
        public const string Exists = "WorkOrder with similar appointment is aready created";

        public const string CannotDispatch = "WorkOrder cannot be dispatched";
    }

    public static class Identity
    {
        public const string InvalidEmailOrPassword = "Invalid email or password";

        public const string SimilarEmailExists = "User with similar email already exists";
    }

    public static class PharmacyUser
    {
        public const string DoesNotHavePharmacy = "User does not have pharmacy";
    }

    public static class Http
    {
        public const string ServiceTemporarilyUnavailable = "Service is temporalily unavailable";
    }
}
