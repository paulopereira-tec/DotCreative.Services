using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Factories;
using DotCreative.Services.BaaSTests.BancoInter.Abstractions;
using DotCreative.Services.BaaSTests.FakeBuilders;
using DotCreative.Services.BaaSTests.Interfaces;
using FluentAssertions;

namespace DotCreative.Services.BaaSTests.BancoInter;

/// <summary>
/// Define os testes 
/// </summary>
[TestFixture]
internal class RequestTransactionTest : TransactionSetupAbstraction, IRequestTransactionTest
{
    /// <summary>
    /// Espera sucesso ao recuperar os dados do boleto
    /// </summary>
    [TestCase("5e09b170-7f26-484a-8ce9-4b8486271d6e", ETransactionStatus.Canceled, 82.04)]
    [TestCase("d87381e9-8c93-433d-aecc-4551520ba95a", ETransactionStatus.Canceled, 48.38)]
    [TestCase("d226e600-1028-4484-8930-c7166c1eb3cd", ETransactionStatus.Canceled, 96.75)]
    public async Task MustBeSuccessOnRequestBilletData(string code, ETransactionStatus status, decimal amount)
    {
        // Arrange
        TransactionInfo transactionInfo = new TransactionInfoFakeBuilder()
            .SetCode(code)
            .SetType(ETransactionType.RequestBilletData)
            .Build(_platformResult);

        // Act
        TransactionResult? result = await TransactionFactory.ExecuteAsync(transactionInfo);

        // Assert
        result.Should().NotBeNull();
        result.Pix.Should().NotBeNull();
        result.Billet.Should().NotBeNull();
        result.Status.Should().Be(status);
        result.Amount.Should().Be(amount);
        result.Code.Should().Be(code);
    }

}
