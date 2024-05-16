namespace DotCreative.Services.BaaSTests.Interfaces;

internal interface IPlatformTest
{
    /// <summary>
    /// Espera sucesso ao testar o acesso e autorização com o banco para retorno do token.
    /// </summary>
    void MustBeSuccessOnAuthorization();
}
