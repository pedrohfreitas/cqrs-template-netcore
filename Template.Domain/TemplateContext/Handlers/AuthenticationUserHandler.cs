using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Template.Domain.TemplateContext.Commands.Authenticate.Contract;
using Template.Domain.TemplateContext.Commands.Authenticate.Inputs;
using Template.Domain.TemplateContext.Entities;
using Template.Domain.TemplateContext.Repositories;
using Template.Shared.Commands;
using Template.Shared.Security;
using Template.Shared.FluentValidator;

namespace Template.Domain.TemplateContext.Handlers
{
    public class AuthenticateUserHandler :
        ICommandHandler<AuthenticateUserCommand>,
        ICommandHandler<ForgetPasswordCommand>,
        ICommandHandler<ResetPasswordCommand>
    {
        private readonly IAuthenticateRepository _repository;
        private readonly TokenOptions _tokenOptions;
        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public AuthenticateUserHandler(IOptions<TokenOptions> jwtOptions, IAuthenticateRepository repository)
        {
            _repository = repository;
            _tokenOptions = jwtOptions.Value;

            if (_tokenOptions == null) throw new ArgumentNullException(nameof(_tokenOptions));

            if (_tokenOptions.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("O período deve ser maior que zero", nameof(TokenOptions.ValidFor));

            if (_tokenOptions.SigningCredentials == null)
                throw new ArgumentNullException(nameof(SigningCredentials));

            if (_tokenOptions.JtiGenerator == null)
                throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
        }

        public ICommandResult Handle(AuthenticateUserCommand command)
        {
            if(command == null)
                return new CommandResult(false, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var contract = new AuthenticateUserCommandContract(command);

            if (contract.Contract.Invalid)
                return new CommandResult( false, contract.Contract.Notifications);

            //Busca o Usuário no banco de dados
            Usuario _usuario = _repository.Authenticate(command.CPF, command.Senha);

            if(_usuario == null)
                return new CommandResult(false, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, command.CPF),
                new Claim(JwtRegisteredClaimNames.NameId, command.CPF),
                new Claim(JwtRegisteredClaimNames.Email, command.CPF),
                new Claim(JwtRegisteredClaimNames.Sub, command.CPF),
                new Claim(JwtRegisteredClaimNames.Jti, _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                new Claim("Nome", _usuario.Nome),
                new Claim("IdPessoa", _usuario.IdPessoa.ToString()),
                new Claim("IdPessoaPai", _usuario.IdPessoaPai.ToString()),
            };

            var jwt = new JwtSecurityToken(
                   issuer: _tokenOptions.Issuer,
                   audience: _tokenOptions.Audience,
                   claims: claims.AsEnumerable(),
                   notBefore: _tokenOptions.NotBefore,
                   expires: _tokenOptions.Expiration,
                   signingCredentials: _tokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = _usuario.Id,
                    name = _usuario.Nome,
                    idPessoaPai = _usuario.IdPessoaPai,
                    idPessoa = _usuario.IdPessoa,
                    firstName = _usuario.FirstName,
                    imagem = _usuario.Imagem
                }
            };

            return new CommandResult(true, response);
        }

        public ICommandResult Handle(ForgetPasswordCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(ResetPasswordCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
