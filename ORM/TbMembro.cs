using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace WEB_API_LIA.ORM;

public partial class TbMembro
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public DateTime DataCadastro { get; set; }

    public string TipoMembro { get; set; } = null!;

    public virtual ICollection<TbEmprestimo> TbEmprestimos { get; set; } = new List<TbEmprestimo>();

    public virtual ICollection<TbReserva> TbReservas { get; set; } = new List<TbReserva>();
}
