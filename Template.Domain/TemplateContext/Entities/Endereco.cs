﻿using System;

namespace Template.Domain.TemplateContext.Entities
{
    public class Endereco
    {
        public long Id { get; set; }
        public long IdPessoa { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
