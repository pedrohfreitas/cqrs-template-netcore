using System;
using System.Collections.Generic;
using System.Text;
using Template.Shared.Commands;
using Template.Shared.FluentValidator;

namespace Template.Domain.TemplateContext.Commands.Authenticate.Inputs
{
    public class ResetPasswordCommand : Notifiable, ICommand
    {
        public string Token { get; set; }
        public string Senha { get; set; }

        bool ICommand.Valid()
        {
            throw new NotImplementedException();
        }
    }
}
