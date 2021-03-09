
using ElectionModels;
using ElectionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryTypeController : BaseController
    {
        private readonly ICategoryTypeRepository categoryTypeRepository;
        public CategoryTypeController(IConfiguration config, ICategoryTypeRepository categoryTypeRepository) : base(config)
        {
            this.categoryTypeRepository = categoryTypeRepository;
        }

        [HttpGet]
        public async Task<List<CategoryType>> Get()
        {
            List<CategoryType> result = await this.categoryTypeRepository.GetAll(Context);
            return result;
        }

        [HttpPost]
        public async Task<CategoryType> Create([FromBody] CategoryType electionObj)
        {
            return await this.categoryTypeRepository.Insert(UOW, electionObj);
        }


        [HttpGet("{id}")]
        public async Task<CategoryType> GetById(int Id)
        {
            CategoryType result = await this.categoryTypeRepository.GetByID(Context, Id);
            return result;
        }


        [HttpDelete("{Id}")]
        public async Task<bool> Delete(int Id)
        {
            CategoryType result = await this.categoryTypeRepository.Delete(UOW, Id);
            return true;
        }

        [HttpPut]
        public async Task<CategoryType> Update([FromBody] CategoryType categoryType)
        {
            CategoryType result = await this.categoryTypeRepository.Update(UOW, categoryType);
            return result;
        }
    }
}
