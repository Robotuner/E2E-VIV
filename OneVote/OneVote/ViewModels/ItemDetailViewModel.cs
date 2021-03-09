using AutoMapper;
using ElectionModels;
using OneVote.Models;
using OneVote.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace OneVote.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private CategoryTypeEnum itemId;
        public CategoryTypeEnum Id { get; set; }
        private IMapper mapper { get; set; }

        public ObservableCollection<CategoryViewModel> CategoryList { get; set; }

        public CategoryTypeEnum ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public ItemDetailViewModel() : base()
        {
            mapper = Utils.CreateMapper();
            this.CategoryList = new ObservableCollection<CategoryViewModel>(); 
        }

        public void LoadItemId(CategoryTypeEnum itemId)
        {
            this.itemId = itemId;
            try
            {
                List<CategoryViewModel> cvmList = DataService.CategoryList.Where(n => n.CategoryType == itemId)?.OrderBy(n => n.Sequence)?.ToList();
                if (cvmList != null)
                {
                    switch(itemId)
                    {
                        case CategoryTypeEnum.measure: 
                            Title = "Measure / Propositions";
                            break;
                        case CategoryTypeEnum.federal: 
                            Title = "Federal Ballots";
                            break;
                        case CategoryTypeEnum.state: 
                            Title = "State Ballots";
                            break;
                        case CategoryTypeEnum.legislative: 
                            Title = "Legislative Ballots";
                            break;
                        case CategoryTypeEnum.judicial: 
                            Title = "Judicial Ballots";
                            break;
                    }

                    this.CategoryList.Clear();                    
                    foreach(CategoryViewModel cvm in cvmList)
                    {
                        if (cvm.Selection != null)
                        {

                        }
                        this.CategoryList.Add(cvm);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public bool ValidateElection(Guid ballotId, string name, Image headshot)
        {
            DateTime submittalDateLocal = DateTime.Now;
            if (DataService.Election == null ||
                submittalDateLocal < DataService.Election.StartDateLocal ||
                submittalDateLocal > DataService.Election.EndDateLocal)
                return false;

            List<Vote> catList = new List<Vote>();
            List<Signature> signatureList = new List<Signature>();
            foreach (CategoryViewModel catvm in this.CategoryList)
            {
                Vote vote = mapper.Map<Vote>(catvm);
                vote.BallotId = ballotId;
                Signature signature = new Signature()
                {
                    BallotId = ballotId,
                    Name = name,
                    ImageArray = null
                };
            }


            return true;
        }

    }
}
