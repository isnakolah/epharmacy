using EPharmacy.Application.Common.Constants;

namespace EPharmacy.Application.Common.Exceptions;

public class CustomException : Exception
{
    public CustomException() : base() 
    {
    }

    public CustomException(string message) : base(message) 
    {
    }

    public CustomException(string message, Exception innerException) : base(message, innerException) 
    { 
    }
}

# region Exceptions
public class PrescriptionAlreadyCancelledException : CustomException
{
    public PrescriptionAlreadyCancelledException() : base(ExceptionMessages.Prescription.Cancelled)
    {
    }
}

public class PrescriptionAlreadyApprovedException : CustomException
{
    public PrescriptionAlreadyApprovedException() : base(ExceptionMessages.Prescription.AlreadyApproved)
    {
    }
}

public class PrescriptionAlreadyQuotedException : CustomException
{
    public PrescriptionAlreadyQuotedException() : base(ExceptionMessages.Prescription.AlreadyQuoted)
    {
    }
}

public class PrescriptionExpiredException : CustomException
{
    public PrescriptionExpiredException() : base(ExceptionMessages.Prescription.Expired)
    {
    }
}

public class QuotationIsCancelledException : CustomException
{
    public QuotationIsCancelledException() : base(ExceptionMessages.Quotation.Cancelled)
    {
    }
}

public class QuotationAlreadyApprovedException : CustomException
{
    public QuotationAlreadyApprovedException() : base(ExceptionMessages.Quotation.AlreadyApproved)
    {
    }
}

public class QuotationNeedsApprovalException : CustomException
{
    public QuotationNeedsApprovalException() : base(ExceptionMessages.Quotation.NeedsApproval)
    {
    }
}

public class WorkOrderExistsException : CustomException
{
    public WorkOrderExistsException() : base(ExceptionMessages.WorkOrder.Exists)
    {
    }
}

public class WorkOrderCannotBeDispatchedException : CustomException
{
    public WorkOrderCannotBeDispatchedException() : base(ExceptionMessages.WorkOrder.CannotDispatch)
    {
    }
}

public class InvalidEmailOrPasswordException : CustomException
{
    public InvalidEmailOrPasswordException() : base(ExceptionMessages.Identity.InvalidEmailOrPassword)
    {
    }
}

public class SimilarEmailExistsException : CustomException
{
    public SimilarEmailExistsException() : base(ExceptionMessages.Identity.SimilarEmailExists)
    {
    }
}

public class UserDoesNotHavePharmacyException : CustomException
{
    public UserDoesNotHavePharmacyException() : base(ExceptionMessages.PharmacyUser.DoesNotHavePharmacy)
    {
    }

}

public class ServiceTemporarilyUnavailableException : CustomException
{
    public ServiceTemporarilyUnavailableException() : base(ExceptionMessages.Http.ServiceTemporarilyUnavailable)
    {
    }
}
#endregion