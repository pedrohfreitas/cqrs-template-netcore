using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Template.Domain.TemplateContext.Entities;
using Template.Domain.TemplateContext.Repositories;

namespace Template.Infra.TemplateContext.Repositories
{
    public partial class AuthenticateRepository : IAuthenticateRepository
    {
        public Usuario Authenticate(string emailcpf, string senha)
        {
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                return conn.Query<Usuario>(SelectUsuario, new { login = emailcpf, senha = senha }).FirstOrDefault();
            }
        }

        public string EmailByTokenResetEmail(string token)
        {
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                return conn.Query<string>(selectEmailByToken, new { Token = token }).FirstOrDefault();
            }
        }

        public bool ResetSenha(string token, string senha)
        {
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                IDbTransaction trans = conn.BeginTransaction();
                try
                {
                    var IdCliente = conn.Query(UpdateTokenESenha, new { Token = token, Senha = senha }, trans);

                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    return false;
                }
            }
        }

        public bool SalvarTokenResetEmail(string email, string token)
        {
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                IDbTransaction trans = conn.BeginTransaction();
                try
                {
                    var IdCliente = conn.Query(UpdateTokenByEmail, new { Token = token, Email = email }, trans);

                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    return false;
                }
            }
        }
    }
}
