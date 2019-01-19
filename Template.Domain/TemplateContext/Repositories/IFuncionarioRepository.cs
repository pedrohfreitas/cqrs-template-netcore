using System.Collections.Generic;
using Template.Domain.TemplateContext.Entities;
using Template.Domain.TemplateContext.Queries.Funcionario;

namespace Template.Domain.TemplateContext.Repositories
{
    public interface IFuncionarioRepository
    {
        long Insert(Pessoa entity);
        bool Update(Pessoa entity);
        IEnumerable<ListFuncionarioQueryResult> List(long idPessoa);
        GetFuncionarioQueryResult Get(long idPessoa);
    }
}
