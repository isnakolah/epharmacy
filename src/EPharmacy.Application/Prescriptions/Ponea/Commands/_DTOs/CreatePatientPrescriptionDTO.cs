using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Patients.Commands.DTOs;
using EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs.Resolvers;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs;

public record class CreatePatientPrescriptionDTO : IMapTo<Prescription>
{
    public CreateConciergePatientDTO Patient { get; init; } = new();
    public List<CreatePharmaceuticalItemDTO> PharmaceuticalItems { get; init; } = new();
    public List<CreateNonPharmaceuticalItemDTO> NonPharmaceuticalItems { get; init; } = new();
    public List<Guid> PharmacyIDs { get; init; } = new();
    public string DeliveryLocation { get; init; } = string.Empty;
    public IEnumerable<string> FileUrls { get; init; } = Array.Empty<string>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePatientPrescriptionDTO, Prescription>()
            .ForMember(dest => dest.Patient,
                opt => opt.MapFrom<PatientResolver>())
            .ForMember(dest => dest.PharmacyPrescriptions,
                opt => opt.MapFrom<PharmacyPrescriptionResolver>())
            .ForMember(dest => dest.DeliveryLocation,
                opt => opt.MapFrom(src => src.DeliveryLocation))
            .ForMember(dest => dest.FileUrls, opt => opt.MapFrom(src => src.FileUrls));
    }

    public Prescription MapToEntity(IMapper mapper)
    {
        return mapper.Map<Prescription>(this);
    }
}