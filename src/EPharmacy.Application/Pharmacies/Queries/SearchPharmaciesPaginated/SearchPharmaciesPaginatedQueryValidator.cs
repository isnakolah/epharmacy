using FluentValidation;

namespace EPharmacy.Application.Pharmacies.Queries.SearchPharmaciesPaginated;

public class SearchPharmaciesPaginatedQueryValidator : AbstractValidator<SearchPharmaciesPaginatedQuery>
{
    public SearchPharmaciesPaginatedQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty().WithMessage("Name to query must be provided");
    }
}
