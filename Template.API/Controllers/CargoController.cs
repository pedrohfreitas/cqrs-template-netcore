using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Template.Domain.TemplateContext.Commands;
using Template.Domain.TemplateContext.Handlers;
using Template.Domain.TemplateContext.Queries;
using Template.Domain.TemplateContext.Repositories;
using Template.Shared.Commands;

namespace Template.API.Controllers
{
    [EnableCors("Cors")]
    public class CargoController : BaseController
    {
        private readonly ICargoRepository _repository;
        private readonly CargoHandler _handler;

        public CargoController(ICargoRepository repository, CargoHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [Route("v1/cargo")]
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
        [Route("v1/cargo/{id}")]
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


        [HttpPost]
        [Route("v1/cargo")]
        public ICommandResult Post([FromBody]InsertCargoCommand command)
        {
            return _handler.Handle(command);
        }
    }
}