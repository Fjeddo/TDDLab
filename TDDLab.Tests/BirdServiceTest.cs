using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace TDDLab.Tests
{
    public class BirdServiceTest
    {
        private readonly BirdService _sut;

        private readonly IBirdRepository _repositoryMock;
        private readonly IBirdValidator _validatorMock;

        public BirdServiceTest()
        {
            _repositoryMock = Substitute.For<IBirdRepository>();
            _validatorMock = Substitute.For<IBirdValidator>();

            _sut = new BirdService(_validatorMock, _repositoryMock);
        }

        [Fact]
        public void Save_valid_bird_should_invoke_repository()
        {
            var bird = new Bird("Tree sparrow");
            _validatorMock.IsValid(Arg.Any<Bird>()).Returns(true);

            _sut.Save(bird);

            _repositoryMock.Received().SaveOrUpdate(bird);
        }

        [Fact]
        public void Save_invalid_bird_should_throw_exception()
        {
            var bird = new Bird("Tree sparrow");
            _validatorMock.IsValid(Arg.Any<Bird>()).Returns(false);

            Action a = () => _sut.Save(bird);

            a.ShouldThrow<ArgumentException>();
            _repositoryMock.DidNotReceive().SaveOrUpdate(Arg.Any<Bird>());
        }
    }
}
