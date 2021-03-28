using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Data;

namespace ElectionAPITest
{
    [TestFixture]
    public class BaseControllerTest
    {
        protected Mock<IConfiguration> mockIConfiguration;
        protected Mock<IDbConnection> mockIDbConnection;
        protected Guid electionId = Guid.Parse("a13acd4a-d415-4b27-afe6-e2310ac71bc6");

        [SetUp]
        public virtual void Setup()
        {
            mockIConfiguration = new Mock<IConfiguration>();
            mockIDbConnection = new Mock<IDbConnection>();
        }
    }
}
