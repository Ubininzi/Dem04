using System;
using System.Collections.Generic;

namespace dem04.EFModel;

public partial class Login
{
    public string Login1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
