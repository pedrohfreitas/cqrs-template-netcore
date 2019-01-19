using System;
using Template.Domain.TemplateContext.Enums;

namespace Template.Domain.TemplateContext.Queries.Funcionario
{
    public class ListFuncionarioQueryResult
    {
        public long Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public PessoaStatus Status { get; set; }
    }
}
