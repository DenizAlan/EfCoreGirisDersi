using System;
using System.Collections.Generic;

namespace NorthwindEfCore.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public int Password { get; set; }
}
