using System;
using System.Collections.Generic;

namespace WEB_API_LIA.ORM;

public partial class TbFuncionario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string Cargo { get; set; } = null!;
}
