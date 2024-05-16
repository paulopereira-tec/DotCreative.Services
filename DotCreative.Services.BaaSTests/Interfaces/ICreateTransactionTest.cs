namespace DotCreative.Services.BaaSTests.Interfaces;

/// <summary>
/// Define os testes que deverão ser realizados por padrão nas transações de todas as plataformas.
/// </summary>
internal interface ICreateTransactionTest
{
    /// <summary>
    /// Espera sucesso ao executar a criação de um boleto.
    /// </summary>
    Task MustBeSuccessOnCreateBillet();
}
