using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Factories;
using DotCreative.Services.BaaSTests.BancoInter.Abstractions;
using DotCreative.Services.BaaSTests.FakeBuilders;
using DotCreative.Services.BaaSTests.Interfaces;
using FluentAssertions;

namespace DotCreative.Services.BaaSTests.BancoInter;

/// <summary>
/// Define os testes padrões a serem executados com as transações.
/// </summary>
[TestFixture]
internal class CreateTransactionTest : TransactionSetupAbstraction, ICreateTransactionTest
{
    /// <summary>
    /// Espera sucesso ao tentar criar um boleto com PIX.
    /// </summary>
    [Test]
    public async Task MustBeSuccessOnCreateBillet()
    {
        // Arrange
        TransactionInfo transactionInfo = new TransactionInfoFakeBuilder()
            .WithPenalty(true)
            .WithDiscount(false)
            .SetType(ETransactionType.CreateBillet)
            .Build(_platformResult);

        // Act
        TransactionResult result = await TransactionFactory.ExecuteAsync(transactionInfo);

        // Assert
        result.Should().NotBeNull();
    }
}
