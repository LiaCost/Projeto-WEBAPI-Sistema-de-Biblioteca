using System;
using System.Collections.Generic;

namespace WEB_API_LIA.ORM;

public partial class TbCategoria
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;
}
