using Template.Shared.FluentValidator.Validation;

namespace Template.Domain.TemplateContext.Commands
{
    public class CargoContract : IContract
    {
        public ValidationContract Contract { get; set; }

        public CargoContract()
        {

        }

        public CargoContract(InsertCargoCommand command)
        {
            Contract = new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(command.Nome, "Nome", "Informe o Nome do Cargo.")
                .IsNotNullOrEmpty(command.Descricao, "Descricao", "Informe a descrição do Cargo.");
        }

        //public CargoContract(UpdateCargoCommand command)
        //{
        //    Contract = new ValidationContract()
        //        .Requires()
        //        .IsNotNullOrEmpty(command.Nome, "Nome", "Informe o Nome do Cargo.")
        //        .IsNotNullOrEmpty(command.Descricao, "Descricao", "Informe a descrição do Cargo.");
        //}
    }
}
