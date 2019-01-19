using System;

namespace Template.Domain.TemplateContext.Queries
{
    public class ListCargoQueryResult
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Status { get; set; }


    }
}
