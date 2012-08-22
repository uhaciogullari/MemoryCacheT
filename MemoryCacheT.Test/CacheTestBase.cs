using System;
using Moq;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    internal class CacheTestBase
    {
        protected ICache<string, int> Cache;
        protected Mock<ITimer> TimerMock;
        protected Mock<IDateTimeProvider> DateTimeProviderMock;
        protected TimeSpan TimerInterval;

        [SetUp]
        public void Setup()
        {
            TimerMock = new Mock<ITimer>(MockBehavior.Strict);
            DateTimeProviderMock = new Mock<IDateTimeProvider>(MockBehavior.Strict);
            TimerInterval = new TimeSpan(0, 0, 1, 0);

            TimerMock.SetupSet(mock => mock.Interval = It.Is<double>(interval => interval > default(double)));
            TimerMock.Setup(mock => mock.Start()).Verifiable();

            Cache = new Cache<string, int>(TimerMock.Object, DateTimeProviderMock.Object, TimerInterval);
            FinalizeSetup();
        }

        protected virtual void FinalizeSetup() { }

        [TearDown]
        public void TearDown()
        {
            TimerMock.VerifyAll();
            DateTimeProviderMock.VerifyAll();
            FinalizeTearDown();
        }

        protected virtual void FinalizeTearDown() { }
    }
}