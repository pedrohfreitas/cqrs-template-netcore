using System.Collections;
using System.Collections.Generic;
using Template.Domain.TemplateContext.Entities;
using Template.Domain.TemplateContext.Queries;

namespace Template.Domain.TemplateContext.Repositories
{
    public interface ICargoRepository
    {
        long Insert(Cargo entity);
        bool Update(Cargo entity);
        IEnumerable<ListCargoQueryResult> List(long idPessoa);
        GetCargoQueryResult Get(long idPessoa);

    }
}
