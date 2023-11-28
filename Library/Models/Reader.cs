using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Reader
{
    public int ReaderId { get; set; }

    public string ReaderName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string SurName { get; set; } = null!;

    public string ReaderEmail { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Extradition> Extraditions { get; } = new List<Extradition>();
}
