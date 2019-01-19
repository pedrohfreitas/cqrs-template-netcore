using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Template.Domain.TemplateContext.Handlers;
using Template.Domain.TemplateContext.Commands.Authenticate.Inputs;
using Template.Domain.TemplateContext.Entities;
using Template.Shared.Commands;
using Template.Domain.TemplateContext.Repositories;
using Microsoft.AspNetCore.Cors;

namespace Template.API.Controllers
{
    [EnableCors("Cors")]
    public class AccountController : Controller
    {
        private readonly AuthenticateUserHandler _handler;
        private readonly IAuthenticateRepository _authenticateRepository;

        public AccountController(AuthenticateUserHandler authenticationHandler, IAuthenticateRepository authenticateRepository)
        {
            _handler = authenticationHandler;
            _authenticateRepository = authenticateRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/authenticate")]
        public async Task<ICommandResult> Post([FromForm] AuthenticateUserCommand command)
        {
            var result = _handler.Handle(command);
            return result;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/forgetpassword")]
        public async Task<ICommandResult> ForgetPassword([FromBody]ForgetPasswordCommand command)
        {
            var result = _handler.Handle(command);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/emailbytoken")]
        public async Task<string> EmailByToken(string token)
        {
            return _authenticateRepository.EmailByTokenResetEmail(token);
;        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/resetpassword")]
        public async Task<ICommandResult> ResetPassword([FromBody]ResetPasswordCommand command)
        {
            var result = _handler.Handle(command);
            return result;
        }
    }
}
