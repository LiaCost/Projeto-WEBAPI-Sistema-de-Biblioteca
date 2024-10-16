using System;
using System.Collections.Generic;

namespace WEB_API_LIA.ORM;

public partial class TbEmprestimo
{
    public int Id { get; set; }

    public DateTime DataEmprestimo { get; set; }

    public DateTime DataDevolucao { get; set; }

    public int FkMembro { get; set; }

    public int FkLivro { get; set; }

    public virtual TbLivro FkLivroNavigation { get; set; } = null!;

    public virtual TbMembro FkMembroNavigation { get; set; } = null!;
}
