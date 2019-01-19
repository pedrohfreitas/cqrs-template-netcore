using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.TemplateContext.Repositories;
using Template.Shared.FluentValidator;

namespace Template.Domain.TemplateContext.Handlers
{
    public class FuncionarioHandler
    {
        private readonly IMapper _mapper;
        private readonly IFuncionarioRepository _repository;

        public FuncionarioHandler(IMapper mapper, IFuncionarioRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
