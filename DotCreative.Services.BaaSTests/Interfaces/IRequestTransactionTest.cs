using DotCreative.Services.BaaS.Domain.Enums;

namespace DotCreative.Services.BaaSTests.Interfaces;

/// <summary>
/// Define os testes que deverão ser realizados por padrão nas transações de todas as plataformas.
/// </summary>
internal interface IRequestTransactionTest
{
    /// <summary>
    /// Espera sucesso ao executar a criação de um boleto.
    /// </summary>
    Task MustBeSuccessOnRequestBilletData(string code, ETransactionStatus stauts, decimal amount);
}
