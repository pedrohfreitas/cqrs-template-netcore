using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.TemplateContext.Entities;
using Template.Domain.TemplateContext.Enums;

namespace Template.Domain.TemplateContext.Queries.Funcionario
{
    public class GetFuncionarioQueryResult
    {
        public long Id { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public PessoaStatus Status { get; set; }
        public TipoPessoa IdTipoPessoa { get; set; }

        public Endereco Endereco { get; set; }
        public List<Telefone> Telefones { get; set; }

        public Usuario Usuario { get; set; }
        public PessoaFuncionario PessoaFuncionario { get; set; }
    }
}
