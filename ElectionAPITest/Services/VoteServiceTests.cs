using ElectionAPI.Service;
using ElectionModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionAPITest.Services
{
    [TestFixture]
    public class VoteServiceTests : BaseServiceTests
    {
        private VoteService voteService = new VoteService();

        [SetUp]
        public override void Setup()
        {
            base.Setup();
        }

        [Test]
        public async Task TestGetAllByElectionId()
        {
            try
            {
                List<Vote> ans = (await voteService.GetAllByElectionId(Context, DefaultElectionId)).ToList();
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
                List<Vote> ans = (await voteService.GetAllByCategoryType(Context, DefaultElectionId, 2)).ToList();
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
                List<Vote> ans = (await voteService.GetAll(Context, DefaultElectionId, PresCategoryId)).ToList();
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
        public async Task TestGetByBallotID()
        {
            try
            {
                Guid ballotId = Guid.Empty;
                List<Vote> ans = (await voteService.GetByBallotID(Context, ballotId)).ToList();
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
        public async Task TestGetVoteSummary()
        {
            try
            {
                List<VoteResult> ans = (await voteService.GetVoteSummary(Context, DefaultElectionId)).ToList();
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

        private async Task<List<Ticket>> GetTicketsByCategory(Guid catId)
        {
            TicketService tservice = new TicketService();
            List<Ticket> tlist = (await tservice.GetByElection(UOW.Context, DefaultElectionId)).ToList();
            return tlist.Where(n => n.CategoryId == catId)?.ToList();
        }

        private async Task<int> GetCurrentVoteSummary(Guid candidateId)
        {
            List<VoteResult> allCandidates = (await voteService.GetVoteSummary(Context, DefaultElectionId)).ToList();
            VoteResult vr = allCandidates.FirstOrDefault(n => n.Id == candidateId);
            return vr == null ? 0 : vr.Count;
        }

        [Test]
        public async Task TestInsertUpdateVote()
        {
            try
            {
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
                int countBeforeInsert1 = await GetCurrentVoteSummary(BidenTicketId);
                Vote inserted = await voteService.Insert(UOW, vote);
                Assert.IsNotNull(inserted);
                Assert.IsTrue(inserted.Id != Guid.Empty, "Expect inserted vote Id not be empty");
                int countAfterInsert1 = await GetCurrentVoteSummary(BidenTicketId);
                Assert.IsTrue(countAfterInsert1 == countBeforeInsert1 + 1, "Expected trigger to increase the count in VoteResult Table");
                // now change the ballot selection to candidate2
                vote.Id = inserted.Id;
                vote.VoteStatus = (int)VoteStatusEnum.choiceRejected;

                Vote updated = await voteService.Update(UOW, vote);
                Assert.IsNotNull(updated);
                Assert.IsTrue(inserted.Id == updated.Id, "Expect inserted and updated Id to be the same");
                int countAfterUpdate1 = await GetCurrentVoteSummary(BidenTicketId);

                Assert.IsTrue(countBeforeInsert1 == countAfterUpdate1, "Expected trigger to decrease the count  in VoteResult Table when VoteStatus changed");

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
                    SelectionId = BidenTicketId,
                    VoteStatus = 0,
                    ApprovalDate = DateTime.Now
                }
            };
            int countBeforeInsert1 = await GetCurrentVoteSummary(BidenTicketId);   

            UOW.BeginTransaction();
            List<Vote> results = await voteService.InsertElection(UOW, votes);
            int countAfterInsert1 = await GetCurrentVoteSummary(BidenTicketId);
            Assert.IsNotNull(results, "Expected votes to be inserted");
            Assert.IsTrue(countAfterInsert1 == countBeforeInsert1 + 1, "Expecte VoteResult Table to be increased by 1 votes for candidate");
            UOW.CloseTransaction();
        }
    }
}
