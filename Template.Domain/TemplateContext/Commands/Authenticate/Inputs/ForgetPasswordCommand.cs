using System;
using System.Collections.Generic;
using System.Text;
using Template.Shared.Commands;
using Template.Shared.FluentValidator;

namespace Template.Domain.TemplateContext.Commands.Authenticate.Inputs
{
    public class ForgetPasswordCommand : Notifiable, ICommand
    {
        public string Email { get; set; }

        bool ICommand.Valid()
        {
            throw new NotImplementedException();
        }
    }
}
