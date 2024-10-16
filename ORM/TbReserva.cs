using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace WEB_API_LIA.ORM;

public partial class TbReserva
{
    public int Id { get; set; }

    public DateTime DataReserva { get; set; }

    public int FkMembro { get; set; }

    public int FkLivro { get; set; }

    public virtual TbLivro FkLivroNavigation { get; set; } = null!;

    public virtual TbMembro FkMembroNavigation { get; set; } = null!;
}
