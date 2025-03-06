using System;
using System.Collections.Generic;

namespace Pr4_Partners.Models;

public partial class TypesOfPartner
{
    public int Id { get; set; }

    public string TypeOfPartner { get; set; } = null!;

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}
