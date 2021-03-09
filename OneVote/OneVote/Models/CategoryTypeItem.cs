using ElectionModels;
using System;

namespace OneVote.Models
{
    public class CategoryTypeItem
    {
        public CategoryTypeEnum Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string CatType 
        {
            get 
            {
                return ((int)Id).ToString(); 
            }
        }

        public int Selected { get; set; }
        public int Total { get; set; }
    }
}