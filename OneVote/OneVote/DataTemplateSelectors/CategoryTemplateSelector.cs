using OneVote.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace OneVote.DataTemplateSelectors
{
    public class CategoryTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MeasureTemplate { get; set; }
        public DataTemplate FederalTemplate { get; set; }
        public DataTemplate JudicialTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            try
            {
                if (item is CategoryViewModel categoryViewModel)
                {
                    switch (categoryViewModel.CategoryType)
                    {
                        case ElectionModels.CategoryTypeEnum.measure:
                            return MeasureTemplate;
                        case ElectionModels.CategoryTypeEnum.judicial:
                            return JudicialTemplate;
                        default: return FederalTemplate;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
