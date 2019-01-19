using System;

namespace Template.Domain.TemplateContext.Entities
{
    public class Cargo
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public long IdPessoa { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
