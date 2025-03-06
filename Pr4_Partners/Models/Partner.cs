using System;
using System.Collections.Generic;

namespace Pr4_Partners.Models;

public partial class Partner
{
    public int Id { get; set; }

    public int IdTypeOfPartner { get; set; }

    public string Name { get; set; } = null!;

    public string LegalAdress { get; set; } = null!;

    public string Tin { get; set; } = null!;

    public string NameOfDirector { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public short? Rating { get; set; }

    public virtual TypesOfPartner IdTypeOfPartnerNavigation { get; set; } = null!;

    public virtual ICollection<PartnersProduct> PartnersProducts { get; set; } = new List<PartnersProduct>();
}
