using Moq;
using PlanoDeContas.Application.Services;
using PlanoDeContas.Domain.Entities;
using PlanoDeContas.Domain.Enums;
using PlanoDeContas.Domain.Interfaces;

namespace PlanoDeContas.Tests
{
    [TestFixture]
    public class PlanoDeContaServiceTests
    {
        private Mock<IPlanoDeContaRepository> _repositoryMock;
        private PlanoDeContaService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IPlanoDeContaRepository>();
            _service = new PlanoDeContaService(_repositoryMock.Object);
        }

        [Test]
        public void AdicionarAsyncDeveLancarExcecaoSeCodigoJaExistir()
        {
            // Arrange
            var contaExistente = new PlanoDeConta { Codigo = "1.1" };
            _repositoryMock.Setup(r => r.ObterPorCodigoAsync("1.1")).ReturnsAsync(contaExistente);

            var novaConta = new PlanoDeConta { Codigo = "1.1", Descricao = "Duplicado", Tipo = TipoContaEnum.Receita };

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.AdicionarAsync(novaConta));
            Assert.That(ex.Message, Is.EqualTo("Já existe uma conta com esse código."));
        }

        [Test]
        public async Task AdicionarAsync_DeveAdicionarConta_SeValida()
        {
            // Arrange
            var novaConta = new PlanoDeConta { Codigo = "1.2", Descricao = "Nova Conta", Tipo = TipoContaEnum.Receita };
            _repositoryMock.Setup(r => r.ObterPorCodigoAsync("1.2")).ReturnsAsync(default(PlanoDeConta));


            // Act
            await _service.AdicionarAsync(novaConta);

            // Assert
            _repositoryMock.Verify(r => r.AdicionarAsync(novaConta), Times.Once);
        }

        [Test]
        public async Task ObterProximoCodigoAsync_DeveRetornarProximoCodigoCorreto()
        {
            // Arrange
            var contasFilhas = new List<PlanoDeConta>
            {
                new() { Codigo = "2.2.1" },
                new() { Codigo = "2.2.2" },
                new() { Codigo = "2.2.7" }
            };

            _repositoryMock.Setup(r => r.ListarPorPaiIdAsync(5)).ReturnsAsync(contasFilhas);

            // Act
            var proximoCodigo = await _service.ObterProximoCodigoAsync(5);

            // Assert
            Assert.That(proximoCodigo, Is.EqualTo("2.2.8"));
        }
    }
}
