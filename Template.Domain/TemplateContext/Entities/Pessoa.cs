using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.TemplateContext.Enums;

namespace Template.Domain.TemplateContext.Entities
{
    public class Pessoa
    {
        public long Id { get; set; }
        public long IdPessoaPai { get; set; }
        public string CPFCNPJ { get; set; }
        public string RGIE { get; set; }
        public string NomeRazao { get; set; }
        public string NomeFantasia { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string DataNascimentoString
        {
            get
            {
                return DataNascimento != null ? DataNascimento.Value.ToString("dd/MM/yyyy") : "";
            }
        }
        public DateTime DataCadastro { get; set; }
        public PessoaStatus Status { get; set; }
        public TipoPessoa IdTipoPessoa { get; set; }

        public Endereco Endereco { get; set; }
        public List<Telefone> Telefones { get; set; }

        public Usuario Usuario { get; set; }
        public PessoaJuridica PessoaJuridica { get; set; }
        public PessoaFuncionario PessoaFuncionario { get; set; }
    }
}
