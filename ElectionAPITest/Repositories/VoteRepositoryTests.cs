using ElectionAPI.Repository;
using ElectionAPI.Service;
using ElectionAPITest.Services;
using ElectionModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPITest.Repositories
{
    [TestFixture]
    public class VoteRepositoryTests : BaseServiceTests
    {
        protected VoteRepository voteRepository;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            voteRepository = new VoteRepository(null, new VoteService());
        }

        [Test]
        public async Task TestGetAllByElectionId()
        {
            try
            {
                List<Vote> ans = (await voteRepository.GetAllByElectionId(Context, DefaultElectionId)).ToList();
                if (ans != null)
                {
                    Assert.IsNotNull(ans, "Expected Vote from default dataset - Save a ballot to get votes back!");
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestGetAllByCategoryType()
        {
            try
            {
                List<Vote> ans = (await voteRepository.GetAllByCategoryType(Context, DefaultElectionId, 2)).ToList();
                if (ans != null)
                {
                    Assert.IsNotNull(ans, "Expected Vote from default dataset- Save a ballot to get votes back!");
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestGetAllByCategory()
        {
            try
            {
                List<Vote> ans = (await voteRepository.GetAll(Context, DefaultElectionId, PresCategoryId)).ToList();
                if (ans != null)
                {
                    Assert.IsNotNull(ans, "Expected Vote from default dataset- Save a ballot to get votes back!");
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        //[Test]
        //public async Task TestGetByBallotID()
        //{
        //    try
        //    {
        //        Guid ballotId = Guid.Empty;
        //        List<Vote> ans = (await voteRepository.GetByBallotID(Context, ballotId)).ToList();
        //        if (ans != null)
        //        {
        //            Assert.IsNotNull(ans, "Expected Vote from default dataset- Save a ballot to get votes back!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
        //    }
        //}

        [Test]
        public async Task TestGetVoteSummary()
        {
            try
            {
                List<VoteResult> ans = (await voteRepository.GetVoteSummary(Context, DefaultElectionId)).ToList();
                if (ans != null)
                {
                    Assert.IsNotNull(ans, "Expected Vote from default dataset- Save a ballot to get votes back!");
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestInsertUpdateVote()
        {
            try
            {
                // now add a vote for candidate1
                Vote vote = new Vote()
                {
                    ElectionId = DefaultElectionId,
                    BallotId = Guid.NewGuid(),
                    CategoryId = PresCategoryId,
                    CategoryTypeId = 2,
                    SelectionId = BidenTicketId,
                    VoteStatus = 0,
                    ApprovalDate = DateTime.Now
                };
                UOW.BeginTransaction();
                Vote inserted = await voteRepository.Insert(UOW, vote);
                Assert.IsNotNull(inserted);
                Assert.IsTrue(inserted.Id != Guid.Empty, "Expect inserted vote Id not be empty");
                // now change the ballot selection to candidate2
                vote.Id = inserted.Id;
                vote.VoteStatus = (int)VoteStatusEnum.choiceRejected;

                Vote updated = await voteRepository.Update(UOW, vote);
                Assert.IsNotNull(updated);
                Assert.IsTrue(inserted.Id == updated.Id, "Expect inserted and updated Id to be the same");
                UOW.CloseTransaction();
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestInsertElection()
        {
            Guid BallotId = Guid.NewGuid();
            List<Vote> votes = new List<Vote>()
            {
                new Vote()
                {
                    ElectionId = DefaultElectionId,
                    BallotId = BallotId,
                    CategoryId = PresCategoryId,
                    CategoryTypeId = 2,
                    SelectionId = TrumpTicketId,
                    VoteStatus = 0,
                    ApprovalDate = DateTime.Now
                }
            };
 
            UOW.BeginTransaction();
            List<Vote> results = await voteRepository.InsertElection(UOW, votes);
            Assert.IsNotNull(results, "Expected votes to be inserted");
            UOW.CloseTransaction();
        }
    }
}
