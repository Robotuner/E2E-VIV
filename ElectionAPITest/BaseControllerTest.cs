using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Data;

namespace ElectionAPITest
{
    [TestFixture]
    public class BaseControllerTest
    {
        protected Mock<IConfiguration> mockIConfiguration;
        protected Mock<IDbConnection> mockIDbConnection;

        [SetUp]
        public virtual void Setup()
        {
            mockIConfiguration = new Mock<IConfiguration>();
            mockIDbConnection = new Mock<IDbConnection>();
        }
    }
}
