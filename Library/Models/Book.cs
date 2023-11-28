using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Book
{
    public string Isbn { get; set; } = null!;

    public string BookName { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int? Provisioner { get; set; }

    public int? Genrei { get; set; }

    public int YearIzd { get; set; }

    public int? Price { get; set; }

    public int? Count { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Extradition> Extraditions { get; } = new List<Extradition>();

    public virtual Genre? GenreiNavigation { get; set; }

    public virtual Provisioner? ProvisionerNavigation { get; set; }
}
