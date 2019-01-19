using AutoMapper;
using Newtonsoft.Json;
using System;
using Template.Domain.TemplateContext.Commands;
using Template.Domain.TemplateContext.Entities;
using Template.Domain.TemplateContext.Repositories;
using Template.Shared.Commands;
using Template.Shared.FluentValidator;

namespace Template.Domain.TemplateContext.Handlers
{
    public class CargoHandler : ICommandHandler<InsertCargoCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICargoRepository _repository;

        public CargoHandler(IMapper mapper, ICargoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public ICommandResult Handle(InsertCargoCommand command)
        {
            try
            {
                var contract = new CargoContract(command);

                if (contract.Contract.Invalid)
                {
                    return new CommandResult(false, "Command invalid", contract.Contract.Notifications);
                }

                //Automapper do command para Entity
                var entity = _mapper.Map<Cargo>(command);

                entity.Id = _repository.Insert(entity);

                return new CommandResult(true, "Sucesso", entity);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, "Erro ao inserir o cargo");
            }
            
        }
    }
}
