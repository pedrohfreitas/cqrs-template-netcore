using System;

namespace Template.Domain.TemplateContext.Entities
{
    public class PessoaJuridica
    {
        public long Id { get; set; }
        public long IdPessoa { get; set; }
        public string Contato { get; set; }
        public string CNAE { get; set; }
        public string Nire { get; set; }
        public DateTime? DataNire { get; set; }
        public string InscricaoMunicipal { get; set; }
        public bool IsentoIE { get; set; }
        public bool OptanteSimples { get; set; }
    }
}
