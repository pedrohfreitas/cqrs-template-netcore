using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Template.Domain.TemplateContext.Entities;
using Template.Domain.TemplateContext.Queries.Funcionario;
using Template.Domain.TemplateContext.Repositories;

namespace Template.Infra.TemplateContext.Repositories
{
    public partial class FuncionarioRepository : IFuncionarioRepository
    {
        public long Insert(Pessoa entity)
        {
            throw new NotImplementedException();
        }

        public GetFuncionarioQueryResult Get(long idPessoa)
        {
            var dictionary = new Dictionary<int, GetFuncionarioQueryResult>();

            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                conn.Query<GetFuncionarioQueryResult, Telefone, Endereco, PessoaFuncionario, GetFuncionarioQueryResult>(
                    cmdGetFuncionario, (p, pt, pe, pf) =>
                    {
                        GetFuncionarioQueryResult funcionario = null;
                        if (!dictionary.TryGetValue((int)p.Id, out funcionario))
                        {
                            dictionary.Add((int)p.Id, funcionario = p);
                        }

                        if (funcionario.Telefones == null)
                            funcionario.Telefones = new List<Telefone>();

                        //Só adicionar o Telefone caso ele não exista
                        if (pt != null && !funcionario.Telefones.Any(t => t.Id == pt.Id))
                        {
                            funcionario.Telefones.Add(pt);
                        }

                        funcionario.Endereco = pe;
                        funcionario.PessoaFuncionario = pf;

                        return funcionario;
                    }, new { IdPessoa = idPessoa });
            }

            return dictionary.Values.FirstOrDefault();
        }

        public IEnumerable<ListFuncionarioQueryResult> List(long idPessoaPai)
        {
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
               var list = conn.Query<ListFuncionarioQueryResult>(cmdListPessoa, new { IdPessoaPai = idPessoaPai });
                return list;
            }
        }

        public bool Update(Pessoa entity)
        {
            throw new NotImplementedException();
        }
    }
}
