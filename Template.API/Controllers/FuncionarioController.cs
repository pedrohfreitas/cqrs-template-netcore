using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Template.Domain.TemplateContext.Handlers;
using Template.Domain.TemplateContext.Repositories;
using Template.Shared.Commands;

namespace Template.API.Controllers
{
    [EnableCors("Cors")]
    public class FuncionarioController : BaseController
    {
        private readonly IFuncionarioRepository _repository;
        private readonly FuncionarioHandler _handler;

        public FuncionarioController(IFuncionarioRepository repository, FuncionarioHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [Route("v1/funcionario")]
        public async Task<ICommandResult> Get()
        {
            try
            {
                return await ResponseSuccess(_repository.List(10002));
            }
            catch (Exception ex)
            {
                return await ResponseError("Erro ao buscar cargo", ex);
            }
        }

        [HttpGet]
        [Route("v1/funcionario/{id}")]
        public async Task<ICommandResult> Get(long id)
        {
            try
            {
                return await ResponseSuccess(_repository.Get(id));
            }
            catch (Exception ex)
            {
                return await ResponseError("Erro ao buscar cargo", ex);
            }
        }
    }
}