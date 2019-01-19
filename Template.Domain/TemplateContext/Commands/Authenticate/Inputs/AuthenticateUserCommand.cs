using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.TemplateContext.Enums;
using Template.Shared.Commands;
using Template.Shared.FluentValidator;

namespace Template.Domain.TemplateContext.Commands.Authenticate.Inputs
{
    public class AuthenticateUserCommand : Notifiable, ICommand
    {
        public string CPF { get; set; }
        public string Senha { get; set; }
        public AplicacaoAcesso AplicacaoAcesso { get; set; }

        bool ICommand.Valid()
        {
            throw new NotImplementedException();
        }
    }
}
