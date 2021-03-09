using ElectionModels;
using ElectionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(IConfiguration config, ICategoryRepository categoryRepository) : base(config)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("GetByElection/{electionId}")]
        public async Task<List<Category>> GetByElection(Guid electionId)
        {
            List<Category> result = await this.categoryRepository.GetByElection(Context, electionId);
            return result;
        }

        [HttpGet("{Id}")]
        public async Task<Category> GetById(Guid Id)
        {
            Category result = await this.categoryRepository.GetByID(Context, Id);
            return result;
        }

        [HttpGet("GetByType/{electionId}/{type}")]
        public async Task<List<Category>> GetByCategoryType(Guid electionId, int type)
        {
            List<Category> result = await this.categoryRepository.GetByType(Context, electionId, type);
            return result;
        }

        [HttpPost]
        public async Task<Category> Create([FromBody] Category category)
        {
            return await this.categoryRepository.Insert(Context, category);
        }

        [HttpPut("{Id}")]
        public async Task<Category> Update([FromBody] Category category)
        {
            Category result = await this.categoryRepository.Update(Context, category);
            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<bool> Delete(Guid Id)
        {
            // removes all records that references this category!
            Category result = await this.categoryRepository.Delete(Context, Id);
            return true;
        }
    }
}
