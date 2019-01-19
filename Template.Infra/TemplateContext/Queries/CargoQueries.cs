namespace Template.Infra.TemplateContext.Repositories
{
    public partial class CargoRepository
    {
        #region [ InsertCargo ]
        private const string cmdInsertCargo = @"
            INSERT INTO dbo.Cargo
            (
                IdPessoa,
                Nome,
                Descricao,
                Status
            )
            OUTPUT inserted.Id
            VALUES
            (
                @IdPessoa,
                @Nome,
                @Descricao,
                @Status
            )
        ";
        #endregion

        #region [ ListCargos ]
        private const string cmdListCargos = @"
            SELECT *
            FROM Cargo
            WHERE 
                IdPessoa = @IdPessoa
            ORDER BY Nome
            ";
        #endregion

        #region [ GetCargo ]
        private const string cmdGetCargo = @"
            SELECT *
            FROM Cargo
            WHERE 
                Id = @Id
                AND IdPessoa = @IdPessoa
        ";
        #endregion
    }
}
