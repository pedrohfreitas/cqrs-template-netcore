using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.TemplateContext.Enums;

namespace Template.Domain.TemplateContext.Entities
{
    public class PessoaFuncionario
    {
        public long Id { get; set; }
        public long IdPessoa { get; set; }

        public TipoCNH? TipoCNH { get; set; }
        public string CNH { get; set; }
        public DateTime? ValidadeCNH { get; set; }
        public Cargo Cargo { get; set; }
    }
}
