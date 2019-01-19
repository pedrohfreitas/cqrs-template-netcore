namespace Template.Infra.TemplateContext.Repositories
{
    public partial class FuncionarioRepository
    {
        #region [ ListPessoa ]
        private const string cmdListPessoa = @"
            SELECT 
                p.Id 
                ,p.NomeRazao AS Nome
                ,p.CPFCNPJ AS CPF
                ,p.Email
                ,p.DataCadastro
                ,p.DataNascimento
                ,p.Status
           FROM dbo.Pessoa p
           WHERE p.IdTipoPessoa = 3
                AND p.IdPessoaPai = @IdPessoaPai
            ";
        #endregion

        #region [ GetFuncionario ]
        private const string cmdGetFuncionario = @"
            SELECT 
                p.Id 
                ,p.NomeRazao AS Nome
                ,p.CPFCNPJ AS CPF
                ,p.RGIE AS RG
                ,p.DataNascimento
                ,p.Email
                ,pt.Id
                ,pt.DDD
                ,pt.Numero
                ,pt.IdTipoTelefone as TipoTelefone
			     ,pe.Id
			     ,pe.CEP
			     ,pe.Estado
			     ,pe.Cidade
			     ,pe.Estado
			     ,pe.Bairro
			     ,pe.Rua
			     ,pe.Numero
			     ,pe.Complemento
			     ,pj.Id
			     ,pj.CNAE
			     ,pj.InscricaoMunicipal
			     ,pj.OptanteSimples
			     ,pj.IsentoIE
			     ,pj.NIRE
			     ,pj.DataNIRE
			     ,pj.Contato
                 ,pf.Id
                 ,pf.TipoCNH
                 ,pf.RegistroCNH AS CNH
                 ,pf.ValidadeCNH
           FROM dbo.Pessoa p
           JOIN dbo.PessoaTelefone pt ON p.id = pt.IdPessoa
		   JOIN dbo.PessoaEndereco pe ON p.Id = pe.IdPessoa
           LEFT JOIN PessoaFuncionario as pf ON p.Id = pf.IdPessoa
           WHERE 
                 p.Id = @IdPessoa
                 AND p.IdPessoaPai = @IdPessoaPai
            ";
        #endregion
    }
}
