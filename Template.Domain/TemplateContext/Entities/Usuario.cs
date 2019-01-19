using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Template.Domain.TemplateContext.Enums;

namespace Template.Domain.TemplateContext.Entities
{
    public class Usuario
    {
        public long Id { get; set; }
        public long IdPessoa { get; set; }
        public long IdPessoaPai { get; set; }

        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public byte[] Imagem { get; set; }
        public UsuarioStatus UsuarioStatus { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<Telefone> Telefones { get; set; }
        public List<Endereco> Enderecos { get; set; }

        public string FirstName
        {
            get
            {
                string[] nomeArray = Nome.Split(' ');
                if (nomeArray != null && nomeArray.Count() > 0)
                {
                    return nomeArray[0];
                }
                return "";
            }
        }
    }
}
