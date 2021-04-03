using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneVote.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public ICommand AccessibilityCommand { get; set; }
        private bool isAccessibility;
        public bool IsAccessibility
        {
            get { return isAccessibility; }
            set
            {
                if (isAccessibility != value)
                {
                    isAccessibility = value;
                    OnPropertyChanged("IsAccessibility");
                }
            }
        }

        public BaseViewModel()
        {
            IsBusy = false;
            IsNotBusy = true;
            isAccessibility = false;
            AccessibilityCommand = new Command(() => {
                IsAccessibility = !IsAccessibility;
            });
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                    IsNotBusy = !value;
                }
            }
        }

        private bool isNotBusy;
        public bool IsNotBusy
        {
            get { return isNotBusy; }
            set
            {
                if (isNotBusy != value)
                {
                    isNotBusy = value;
                    OnPropertyChanged("IsNotBusy");
                }
            }
        }


        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
