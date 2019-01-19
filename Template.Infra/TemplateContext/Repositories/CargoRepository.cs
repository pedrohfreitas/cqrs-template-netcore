using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Template.Domain.TemplateContext.Entities;
using Template.Domain.TemplateContext.Queries;
using Template.Domain.TemplateContext.Repositories;

namespace Template.Infra.TemplateContext.Repositories
{
    public partial class CargoRepository : ICargoRepository
    {
        public long Insert(Cargo entity)
        {
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                IDbTransaction trans = conn.BeginTransaction();
                try
                {
                    var id = conn.Query<long>(cmdInsertCargo, entity, trans).Single();

                    trans.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public IEnumerable<GetCargoQueryResult> Get(long idPessoa)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListCargoQueryResult> List(long idPessoa)
        {
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                var list = conn.Query<ListCargoQueryResult>(cmdListCargos, new { IdPessoa = idPessoa }).ToList();
                return list;
            }
        }

        public bool Update(Cargo entity)
        {
            throw new NotImplementedException();
        }

        GetCargoQueryResult ICargoRepository.Get(long idPessoa)
        {
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                var entity = conn.Query<GetCargoQueryResult>(cmdGetCargo).Single();
                return entity;
            }
        }
    }
}
