﻿namespace EPharmacy.Domain.Entities;

public class Formulation
{
    public Guid ID { get; set; }
    public string Name { get; set; } = default!;

    public ICollection<PharmaceuticalItem> PharmaceuticalItems { get; set; } = default!;
}
