using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.TemplateContext.Enums;

namespace Template.Domain.TemplateContext.Entities
{
    public class Telefone
    {
        public long Id { get; set; }
        public long IdPessoa { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
        public TipoTelefone TipoTelefone { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
