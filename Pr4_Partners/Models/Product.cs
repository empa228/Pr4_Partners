using System;
using System.Collections.Generic;

namespace Pr4_Partners.Models;

public partial class Product
{
    public int Id { get; set; }

    public int IdProductType { get; set; }

    public string Name { get; set; } = null!;

    public string ArticleNumber { get; set; } = null!;

    public decimal MinPriceForPartner { get; set; }

    public virtual ProductType IdProductTypeNavigation { get; set; } = null!;

    public virtual ICollection<PartnersProduct> PartnersProducts { get; set; } = new List<PartnersProduct>();
}
