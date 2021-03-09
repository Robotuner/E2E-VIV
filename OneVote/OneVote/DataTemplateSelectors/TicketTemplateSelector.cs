using OneVote.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace OneVote.DataTemplateSelectors
{
    public class TicketTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TicketWithPartyTemplate { get; set; }
        public DataTemplate SimpleTicketTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            try
            {
                if (item is TicketViewModel ticketViewModel)
                {                   
                    Debug.WriteLine(string.Format("TicketId: {0}  {1}  {2}", ticketViewModel.Id, ticketViewModel.Description, ticketViewModel.Selected));
                    if (string.IsNullOrEmpty(ticketViewModel.Party))
                    {                        
                        return SimpleTicketTemplate;
                    }
                    return TicketWithPartyTemplate;
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
