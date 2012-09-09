using System;
using Moq;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    internal class CacheTestBase
    {
        protected ICache<string, int> _cache;
        protected Mock<ITimer> _timerMock;
        protected Mock<IDateTimeProvider> _dateTimeProviderMock;
        protected TimeSpan _timerInterval;

        [SetUp]
        public void Setup()
        {
            _timerMock = new Mock<ITimer>(MockBehavior.Strict);
            _dateTimeProviderMock = new Mock<IDateTimeProvider>(MockBehavior.Strict);
            _timerInterval = new TimeSpan(0, 0, 1, 0);

            _timerMock.SetupSet(mock => mock.Interval = It.Is<double>(interval => interval > double.Epsilon));
            _timerMock.Setup(mock => mock.Start()).Verifiable();

            _cache = new Cache<string, int>(_timerMock.Object, _dateTimeProviderMock.Object, _timerInterval);
            FinalizeSetup();
        }

        protected virtual void FinalizeSetup() { }

        [TearDown]
        public void TearDown()
        {
            _timerMock.VerifyAll();
            _dateTimeProviderMock.VerifyAll();
            FinalizeTearDown();
        }

        protected virtual void FinalizeTearDown() { }
    }
}