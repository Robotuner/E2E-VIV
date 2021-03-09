using ElectionModels;
using OneVote.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OneVote.Services
{
    public class DataStore : IDataStore
    {
        public List<CategoryTypeItem> Items { get; }

        public DataStore()
        {
            this.Items = new List<CategoryTypeItem>()
            {
                new CategoryTypeItem { Id = CategoryTypeEnum.measure, Text = "Measures / Propositions", Description="Measures." },
                new CategoryTypeItem { Id = CategoryTypeEnum.federal, Text = "Federal Candidates", Description="US Executive and Legislative Branch." },
                new CategoryTypeItem { Id = CategoryTypeEnum.state, Text = "Statewide Candidates", Description="State Executive Branch" },
                new CategoryTypeItem { Id = CategoryTypeEnum.legislative, Text = "Legislative Candidates", Description="State Legislative Branch" },
                new CategoryTypeItem { Id = CategoryTypeEnum.judicial, Text = "Judical Candidates", Description="State Judicial Branch" },
            };          
        }

        //public async Task<IEnumerable<Category>> GetCatalogAsync(CategoryTypeEnum id)
        //{
        //    IEnumerable<Category> catList = DataService.Election.CategoryList.Where(n => n.CategoryTypeId == id)?.OrderBy(n => n.Sequence);
        //    return await Task.FromResult(catList);
        //}
    }
}