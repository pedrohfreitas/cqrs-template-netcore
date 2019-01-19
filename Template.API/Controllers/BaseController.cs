using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Template.Shared.Commands;

namespace Template.API.Controllers
{
    public class BaseController : Controller
    {
        public async Task<ICommandResult> ResponseSuccess(object data)
        {
            return new CommandResult(true, data);
        }

        public async Task<ICommandResult> ResponseError(string message, Exception ex)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == EnvironmentName.Development;

            if(isDevelopment)
                return new CommandResult(false, ex.Message);
            else
                return new CommandResult(false, message);
        }

        public long GetIdPessoaPai()
        {
            var claims = (ClaimsIdentity)User.Identity;
            var pessoa = claims.Claims.Where(p => p.Type == "IdPessoaPai").FirstOrDefault();
            return pessoa != null ? long.Parse(pessoa.Value) : 0;
        }

        public long GetIdPessoaLogada()
        {
            var claims = (ClaimsIdentity)User.Identity;
            var pessoa = claims.Claims.Where(p => p.Type == "IdPessoa").FirstOrDefault();
            return pessoa != null ? long.Parse(pessoa.Value) : 0;
        }
    }
}
