﻿using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.TemplateContext.Commands.Authenticate.Inputs;
using Template.Shared.FluentValidator.Validation;

namespace Template.Domain.TemplateContext.Commands.Authenticate.Contract
{
    public class AuthenticateUserCommandContract : IContract
    {
        public ValidationContract Contract { get; }

        public AuthenticateUserCommandContract(AuthenticateUserCommand command)
        {
            Contract = new ValidationContract()
           .Requires()
           .IsNotNullOrEmpty(command.CPF, "Username", "O campo UserName é obrigatório")
           .IsNotNullOrEmpty(command.Senha, "Password", "O campo Password é obrigatório")
           .HasMinLen(command.Senha, 6, "Password", "O campo Password precisa ter no mínimo 6 caracteres");
        }

        public AuthenticateUserCommandContract(ResetPasswordCommand command)
        {
            Contract = new ValidationContract()
           .Requires()
           .IsNotNullOrEmpty(command.Token, "Token", "O campo Token é obrigatório")
           .IsNotNullOrEmpty(command.Senha, "Password", "O campo Password é obrigatório")
           .HasMinLen(command.Senha, 6, "Password", "O campo Password precisa ter no mínimo 6 caracteres");
        }
    }
}
