using ElectionModels;
using OneVote.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneVote.Services
{
    public interface IDataStore
    {
        List<CategoryTypeItem> Items { get; }   
        //Task<IEnumerable<Category>> GetCatalogAsync(CategoryTypeEnum id);
    }
}
