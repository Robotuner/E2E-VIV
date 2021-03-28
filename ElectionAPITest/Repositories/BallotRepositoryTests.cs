using ElectionAPI.Repository;
using ElectionAPI.Service;
using ElectionAPITest.Services;
using ElectionModels;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ElectionAPITest.Repositories
{
    [TestFixture]
    public class BallotRepositoryTests : BaseServiceTests
    {
        protected BallotRepository ballotRepository;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ballotRepository = new BallotRepository(null, new BallotService());
        }

        [Test]
        public async Task TestGetByElection()
        {
            try
            {
                Ballot ans = await ballotRepository.GetByElection(Context, DefaultElectionId);
                Assert.IsNotNull(ans, "Expect at once current ballot in default dataset - Run Postman InitElectionBallot!");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestGetLastNonce()
        {
            try
            {
                Ballot ans = await ballotRepository.GetByElection(Context, DefaultElectionId);
                Assert.IsNotNull(ans, "Expect at once current ballot in default dataset - Run Postman InitElectionBallot!");
                Assert.IsTrue(ans.Nonce > 0, "Expect Nonce to be > 0");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestInsert()
        {
            try
            {
                Ballot ans = new Ballot()
                {
                    ElectionId = DefaultElectionId,
                    Nonce = 525,
                    BallotChain = "Something"
                };
                Ballot inserted = await ballotRepository.Insert(Context, ans);
                Assert.IsNotNull(inserted, "Expected inserted ballot");
                Assert.IsTrue(ans.Nonce == 525, "Expect Nonce to be 525");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }
    }
}
