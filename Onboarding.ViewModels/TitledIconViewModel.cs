using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Onboarding.Models;
using Onboarding.ViewModels.Annotations;
using PropertyChanged;

namespace Onboarding.ViewModels
{
    public class TitledIconViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private readonly TitledIcon TitledIcon;
        public TitledIconViewModel(TitledIcon titledIcon)
        {
            if (titledIcon == null)
            {
                throw new ArgumentNullException();
            }

            TitledIcon = titledIcon;
        }

        public string Title
        {
            get => TitledIcon.Title;
            set => TitledIcon.Title = value;
        }

        public string ImageSource
        {
            get => TitledIcon.ImageSource;
            set => TitledIcon.ImageSource = value;
        }

        public bool IsSelected
        {
            get => TitledIcon.IsSelected; 
            set => TitledIcon.IsSelected = value;
        }

        [DependsOn(nameof(IsSelected))]
        public bool IsNotSelected => !IsSelected;
    }
}